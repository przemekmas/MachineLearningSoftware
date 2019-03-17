from __future__ import absolute_import
from __future__ import division
from __future__ import print_function

import argparse
import shutil
import sys
import datetime
import os

import tensorflow as tf

experimentsFileName = 'ExperimentsFile.csv'
{0}
{1}

parser = argparse.ArgumentParser()

parser.add_argument(
    '--model_dir', type=str, default='model',
    help='Base directory for the model.')

parser.add_argument(
    '--model_type', type=str, default='{8}',
    help="Valid model types: {{'wide', 'deep', 'wide_deep', 'all'}}.")

parser.add_argument(
    '--train_epochs', type=int, default=40, help='Number of training epochs.')

parser.add_argument(
    '--epochs_per_eval', type=int, default=2,
    help='The number of training epochs to run between evaluations.')

parser.add_argument(
    '--batch_size', type=int, default=40, help='Number of examples per batch.')

parser.add_argument(
    '--train_data', type=str, default='{10}',
    help='Path to the training data.')

parser.add_argument(
    '--test_data', type=str, default='{11}',
    help='Path to the test data.')

_NUM_EXAMPLES = {{
    'train': {6},
    'validation': {7},
}}

availableModels = ["wide", "deep", "wide_deep"]

def build_model_columns():
{2}

{3}

{4}

{5}

  return wide_columns, deep_columns

def build_estimator(model_dir, model_type):
  wide_columns, deep_columns = build_model_columns()
  hidden_units = [100, 75, 50, 25]

  run_config = tf.estimator.RunConfig().replace(
      session_config=tf.ConfigProto(device_count={{'GPU': 0}}))

  if model_type == 'wide':
    return tf.estimator.LinearClassifier(
        model_dir=model_dir,
        feature_columns=wide_columns,
        config=run_config,
        optimizer=tf.train.FtrlOptimizer(
          learning_rate=0.1,
          l1_regularization_strength=10.0,
          l2_regularization_strength=0.0)
		  {13})
  
  elif model_type == 'deep':
    return tf.estimator.DNNClassifier(
        model_dir=model_dir,
        feature_columns=deep_columns,
        hidden_units=hidden_units,
        config=run_config
		{13})
  
  else:
    return tf.estimator.DNNLinearCombinedClassifier(
        model_dir=model_dir,
        linear_feature_columns=wide_columns,
        dnn_feature_columns=deep_columns,
        dnn_hidden_units=hidden_units,
        config=run_config
		{13})


def input_fn(data_file, num_epochs, shuffle, batch_size):
  assert tf.gfile.Exists(data_file), (
      '%s not found. Please make sure that the file exists or '
      'set both arguments --train_data and --test_data.' % data_file)

  def parse_csv(value):
    print('Parsing', data_file)
    columns = tf.decode_csv(value, record_defaults=_CSV_COLUMN_DEFAULTS)
    features = dict(zip(_CSV_COLUMNS, columns))
    labels = features.pop('{9}')
    return features, {12}

  dataset = tf.data.TextLineDataset(data_file)

  if shuffle:
    dataset = dataset.shuffle(buffer_size=_NUM_EXAMPLES['train'])

  dataset = dataset.map(parse_csv, num_parallel_calls=5)

  dataset = dataset.repeat(num_epochs)
  dataset = dataset.batch(batch_size)

  iterator = dataset.make_one_shot_iterator()
  features, labels = iterator.get_next()
  return features, labels

def main(unused_argv):
	if (FLAGS.model_type == "all"):
		for model in availableModels:
			startTraining(model)
	else:
		startTraining(FLAGS.model_type)

def startTraining(model):
  FLAGS.model_type = model
  shutil.rmtree(FLAGS.model_dir, ignore_errors=True)
  model = build_estimator(FLAGS.model_dir, FLAGS.model_type)
  startDateTime = datetime.datetime.now()

  for n in range(FLAGS.train_epochs // FLAGS.epochs_per_eval):
    model.train(input_fn=lambda: input_fn(
        FLAGS.train_data, FLAGS.epochs_per_eval, True, FLAGS.batch_size))

    results = model.evaluate(input_fn=lambda: input_fn(
        FLAGS.test_data, 1, False, FLAGS.batch_size))

    print('Results at epoch', (n + 1) * FLAGS.epochs_per_eval)
    print('-' * 60)

    for key in sorted(results):
      print('%s: %s' % (key, results[key]))

  endDateTime = datetime.datetime.now()
  wideColumns, deepColumns = build_model_columns()

  if FLAGS.model_type == 'wide':
    feature_columns = wideColumns

  elif FLAGS.model_type == 'deep':
    feature_columns = deepColumns

  else:
    feature_columns = wideColumns + deepColumns
  
  feature_spec = tf.feature_column.make_parse_example_spec(feature_columns)
  export_input_fn = tf.estimator.export.build_parsing_serving_input_receiver_fn(feature_spec)
  servable_model_dir = "exported_model"
  servable_model_path = model.export_savedmodel(servable_model_dir, export_input_fn)
  print("Done Exporting Model To Directory - %s", servable_model_path )
  isNewExperimentsFile = False
  
  if (os.path.isfile(experimentsFileName) == False):
    experimentsFile = open(experimentsFileName, 'w')
    isNewExperimentsFile = True
  else:
    experimentsFile = open(experimentsFileName, 'a')

  results["description"] = "";
  results["experiment start date/time"] = startDateTime.strftime("%Y-%m-%d %H:%M:%S");
  results["data pre-processing techniques"] = "";
  results["model used"] = FLAGS.model_type;
  results["model parameters"] = "";
  results["done"] = "Yes";
  results["total train time"] = (endDateTime - startDateTime);
  results["notes"] = "";
  
  if (isNewExperimentsFile == True):
    for key in results:
      experimentsFile.write(str(key))
      experimentsFile.write(',')
      
  experimentsFile.write("\n")
  for key in results:
    experimentsFile.write(str(results[key]))
    experimentsFile.write(',')

  experimentsFile.close()

if __name__ == '__main__':
  tf.logging.set_verbosity(tf.logging.INFO)
  FLAGS, unparsed = parser.parse_known_args()
  tf.app.run(main=main, argv=[sys.argv[0]] + unparsed)
