# Tensorflow Machine learning software/Pre-processing Assistant

The purpose of this repository is to provide software which allows to understand and use object recognition/detection by the use of the Emgu TF library. The open-source software is a windows application that has been written in C#. The software takes advantage of the Emgu TF library which is a C# wrapper for TensorFlow. However, the software also utilises the raw Python TensorFlow framework due to the limitations of the Emgu TF library. Also, the purpose of the software is to provide tools that can be used to apply data pre-processing techniques to CSV/XLSX type datasets that can later be trained with TensorFlow. The software also contains a performance indicator tool that can be used to measure the performance of a predictive model based on the output of the model metrics.

### .NET Framework Version

* The .NET Framework version that is required to build the project is .NET Framework 4.6.1

### Software Screens/Pages

* Change Theme - The change theme page is a very basic screen where you can change the theme/look and feel of the software.
![](https://raw.githubusercontent.com/przemekmas/MachineLearningSoftware/GithubImages/Change_Theme_Screen.png "")

* Exception Log - The exception log page is used for development purposes and helps to identify any exceptions that occur while running the software.
![](https://raw.githubusercontent.com/przemekmas/MachineLearningSoftware/GithubImages/Exception_Log_Screen.png "")

* MNIST - The MNIST page shows a visual representation of the MNIST dataset. It demonstrates how the 28 by 28 images and the corresponding labels are stored as an array within the dataset on a 28 by 28 grid.
![](https://raw.githubusercontent.com/przemekmas/MachineLearningSoftware/GithubImages/MNIST_Screen.png "")

* Object Recognition - The object recognition screen uses Tensorflow to recognise the images. The default retrained model is used, which will be able to recognise flowers. However, the user can also provide their model to recognise any other objects.
![](https://raw.githubusercontent.com/przemekmas/MachineLearningSoftware/GithubImages/Object_Recognition_Screen.png "")

* People Detection - The people detection screen uses Tensorflow to recognise people within a provided image.
![](https://raw.githubusercontent.com/przemekmas/MachineLearningSoftware/GithubImages/People_Detection_Screen.png "")

* Retrain Inception Model - The retrain inception model screen can be used to create a trained inception model for object recognition. The screen uses pip and python to install or update Tensorflow on your machine. After Tensorflow is successfully installed, the user can then choose a folder which should contain subfolders with images that correspond to the folder name. In the end, the user will press the build TensorFlow button to create a trained model that can be used for object recognition.
![](https://raw.githubusercontent.com/przemekmas/MachineLearningSoftware/GithubImages/Retrain_Inception_Model_Screen.png "")

* Wide Deep - This screen uses a wide deep model to predict if an individual earns more than $50k a year. 
![](https://raw.githubusercontent.com/przemekmas/MachineLearningSoftware/GithubImages/Wide_Deep_Screen.png "")

* Performance Indication Tool - The performance indicator tool can be used to measure the performance of a data pre-processing that has been applied to a predictive model.
![](https://raw.githubusercontent.com/przemekmas/MachineLearningSoftware/GithubImages/Performance_Indicator_Tool.png "")

* TensorBoard Tool - The TensorBoard tool can be used to analyse the performance of a predictive model.
![](https://raw.githubusercontent.com/przemekmas/MachineLearningSoftware/GithubImages/TensorBoard_Tool.png "")

* Train Dataset Tool - The train dataset tool can be used to import a CSV or XLSX dataset and create a python train script.
![](https://raw.githubusercontent.com/przemekmas/MachineLearningSoftware/GithubImages/Train_Dataset_Tool.png "")

* Clean Data Tool - The clean data tool can be used to apply data pre-processing techniques to a dataset which is stored in a CSV or XLSX format.
![](https://raw.githubusercontent.com/przemekmas/MachineLearningSoftware/GithubImages/Clean_Data_Tool.png "")

* Search - The search function can be used to search for a screen within the software. It can also be used to search google and navigate to the chosen search result.

### Additional Notes

* Please keep in mind that this is not the official Tensorflow software and it has been created for educational purposes.
* The software is constantly improving. So, if there are any issues, bugs or any other improvements that can be made to the software, please let me know.

### References

* http://www.emgu.com/wiki/index.php/Emgu_TF
* https://github.com/emgucv/emgutf
* https://jamesmccaffrey.wordpress.com/2013/11/23/reading-the-mnist-data-set-with-c/
