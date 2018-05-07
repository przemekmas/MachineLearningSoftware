# Object Class Recognition/Training With Tensorflow

The purpose of this repository is to provide software which allows to understand and use object recognition and training by the use of the Tensorflow framework. The open source software has been created for windows and it has been written within C#. However, the Tensorflow framework works best with python which is why most the tutorials on the internet will be using Linux and python with the use of the Tensorflow framework.

### Software Screens/Pages

* Change Theme - The change theme page is a very basic screen where you can change the theme/look and feel of the software.
* Exception Log - The exception log page is used for development purposes and helps to identify any exceptions that occur while running the software.
* MNIST - The MNIST page shows a visual representation of the MNIST dataset. It demonstrates how the 28 by 28 images and the corresponding labels are stored as an array within the dataset on a 28 by 28 grid.
* Object Recognition - The object recognition screen uses Tensorflow to recognise the images. The default retrained model is used, which will be able to recognise flowers. However, the user can also provide their own model to recognise any other objects.
* People Detection - The people detection screen uses Tensorflow to recognise people within a provided image.
* Retrain Inception Model - The retrain inception model screen can be used to create a trained inception model for object recognition. The screen uses pip and python to install or update Tensorflow on your machine. After Tensorflow is successfully installed, the user can then choose a folder which should contain subfolders with images that correspond to the folder name. In the end the user will press the build tensorflow button to create a trained model that can used for object recognition.
* Search - The search function can be used to search for a screen within the software. It can also be used to search google and navigate to the chosen search result.

### Additional Notes

* Please keep in mind that this is not the offical Tensorflow software and it has been created for educational purposes.
* The software is constantly improving. So, if there are any issues, bugs or any other improvements that can be made to the software, please let me know.

### References

* http://www.emgu.com/wiki/index.php/Emgu_TF
* https://jamesmccaffrey.wordpress.com/2013/11/23/reading-the-mnist-data-set-with-c/