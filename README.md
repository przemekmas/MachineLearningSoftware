# Tensorflow Machine learning software

The purpose of this repository is to provide software which allows to understand and use object recognition and training by the use of the Tensorflow framework. The open source software has been created for windows and it has been written within C#. However, the Tensorflow framework works best with python which is why most the tutorials on the internet will be using Linux and python with the use of the Tensorflow framework.

### .NET Framework

* The .NET Framework required to build the project and execute the software is .NET Framework 4.6.1

### Download Link

Below is the download link to the latest build of the software.

* https://drive.google.com/open?id=1jEFOrdyHmiD5SHJs2pyYhYpL6tyk5_k4

### Software Screens/Pages

* Change Theme - The change theme page is a very basic screen where you can change the theme/look and feel of the software.
![](https://raw.githubusercontent.com/przemekmas/MachineLearningSoftware/GithubImages/Change_Theme_Screen.png "")

* Exception Log - The exception log page is used for development purposes and helps to identify any exceptions that occur while running the software.
![](https://raw.githubusercontent.com/przemekmas/MachineLearningSoftware/GithubImages/Exception_Log_Screen.png "")

* MNIST - The MNIST page shows a visual representation of the MNIST dataset. It demonstrates how the 28 by 28 images and the corresponding labels are stored as an array within the dataset on a 28 by 28 grid.
![](https://raw.githubusercontent.com/przemekmas/MachineLearningSoftware/GithubImages/MNIST_Screen.png "")

* Object Recognition - The object recognition screen uses Tensorflow to recognise the images. The default retrained model is used, which will be able to recognise flowers. However, the user can also provide their own model to recognise any other objects.
![](https://raw.githubusercontent.com/przemekmas/MachineLearningSoftware/GithubImages/Object_Recognition_Screen.png "")

* People Detection - The people detection screen uses Tensorflow to recognise people within a provided image.
![](https://raw.githubusercontent.com/przemekmas/MachineLearningSoftware/GithubImages/People_Detection_Screen.png "")

* Retrain Inception Model - The retrain inception model screen can be used to create a trained inception model for object recognition. The screen uses pip and python to install or update Tensorflow on your machine. After Tensorflow is successfully installed, the user can then choose a folder which should contain subfolders with images that correspond to the folder name. In the end the user will press the build tensorflow button to create a trained model that can used for object recognition.
![](https://raw.githubusercontent.com/przemekmas/MachineLearningSoftware/GithubImages/Retrain_Inception_Model_Screen.png "")

* Wide Deep - This screen uses a wide deep model to predict if an individual earns more than a $50k a year. 
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
* https://jamesmccaffrey.wordpress.com/2013/11/23/reading-the-mnist-data-set-with-c/