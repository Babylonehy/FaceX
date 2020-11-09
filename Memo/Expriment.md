# Traditional method
- [Facial expression recognition using SVM](https://github.com/amineHorseman/facial-expression-recognition-svm)
    > Extract face landmarks using Dlib and train a multi-class SVM classifier to recognize facial expressions (emotions).
    
    > |       Features                          |  7 emotions   |   5 emotions   |
    > |-----------------------------------------|---------------|----------------|
    > | HoG features                            |     29.0%     |      34.4%     |
    > | Face landmarks                          |     39.2%     |      46.9%     |
    > | Face landmarks + HOG                    |     48.2%     |      55.0%     |
    > | Face landmarks + HOG on slinding window |     50.5%     |      59.4%     |

# Learning based
- [resnet-facial-expression](https://github.com/thoo/resnet-facial-expression)
> Deep learning has revitalized computer vision and enhanced the accuracy of machine interpretation on images in recent years. Especially convolutional neural networks (CNN) is so far the go-to machine learning algorithm with great preformance. In this project, I build CNN models to classify the facial expression into six facial emotion categories such as happy (😄) , sad (😢), fear (😨) , surprise (😲), neutral (😐) and angry (😠).

- [Facial expression recognition using CNN in Tensorflow](https://github.com/amineHorseman/facial-expression-recognition-using-cnn)
> Using a Convolutional Neural Network (CNN) to recognize facial expressions from images or video/camera stream.

## Ensemble 
- [Facial Expression Recognition using Residual Masking Network, in PyTorch]()
  - Benchmarking on FER2013

  We benchmark our code thoroughly on two datasets: FER2013 and VEMO. Below are the results and trained weights:


  Model | Accuracy |
  ---------|--------|
  [VGG19](https://drive.google.com/open?id=1FPkwhmel0AiGK3UtYiWCHPi5CYkF7BRc) | 70.80
  [EfficientNet\_b2b](https://drive.google.com/open?id=1pEyupTGQPoX1gj0NoJQUHnK5-mxB8NcS) | 70.80
  [Googlenet](https://drive.google.com/open?id=1LvxAxDmnTuXgYoqBj41qTdCRCSzaWIJr) | 71.97
  [Resnet34](https://drive.google.com/open?id=1iuTkqApioWe_IBPQ7gQHticrVlPA-xz_) | 72.42
  [Inception\_v3](https://drive.google.com/open?id=17mapZKWYMdxGTrbrAbRpfgniT5onmQXO) | 72.72
  [Bam\_Resnet50](https://drive.google.com/open?id=1K_gyarekwIxQMA_fEPJMApgqo3mYaM0H) | 73.14
  [Densenet121](https://drive.google.com/open?id=1f8wUtQj-UatrZtCnkJFcB--X2eJS1m_N) | 73.16
  [Resnet152](https://drive.google.com/open?id=1LBaHaVtu8uKiNsoTN7wl5Pg5ywh-QxRW) | 73.22
  [Cbam\_Resnet50](https://drive.google.com/open?id=1i9zk8sGXiixkQGTA1txBxSuew6z_c86T) | 73.39
  [ResMaskingNet](https://drive.google.com/open?id=1_ASpv0QNxknMFI75gwuVWi8FeeuMoGYy) | 74.14
  [ResMaskingNet + 6](https://drive.google.com/open?id=1y28VHzJcgBpW0Qn_K0XVVd-hxG4feIHG) | 76.82
# mutivarGuassian
- [scipy.stats.multivariate_normal]