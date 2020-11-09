# Fer2013 dataset make
- [Class](https://medium.com/analytics-vidhya/read-fer2013-face-expression-recognition-dataset-using-pytorch-torchvision-9ff64f55018e)

# dataset retrived
- [](http://www.jeffcohn.net/Resources/)


# Expriment Result
- Jaffed: only front face
  - Baseline: CNN, batchsize=7, epoch=30, val_acc=60-70% / with augmentation <60%
  - SVM: 
    ``` python
    param_grid = { "C" : [0.1, 1, 10] , "gamma" : [1, 0.1, 0.01]}
    gs = GridSearchCV(estimator=SVC(kernel='linear'), param_grid=param_grid, scoring='accuracy', refit=True, verbose=2)
    ```
    - original

    ```bash
    0.8117647058823529
    {'C': 0.1, 'gamma': 1}
    [Parallel(n_jobs=1)]: Using backend SequentialBackend with 1 concurrent workers.
    [Parallel(n_jobs=1)]: Done   1 out of   1 | elapsed:    0.0s remaining:    0.0s
    [Parallel(n_jobs=1)]: Done  45 out of  45 | elapsed:    0.2s finished

    0.9069767441860465


    [[9 0 0 0 0 0 0]
    [1 1 0 0 0 0 0]
    [0 0 5 0 0 0 0]
    [0 0 0 3 0 0 0]
    [0 1 0 0 6 0 0]
    [0 0 0 0 1 9 0]
    [0 0 0 0 1 0 6]]


                precision    recall  f1-score   support

            0       0.90      1.00      0.95         9
            1       0.50      0.50      0.50         2
            2       1.00      1.00      1.00         5
            3       1.00      1.00      1.00         3
            4       0.75      0.86      0.80         7
            5       1.00      0.90      0.95        10
            6       1.00      0.86      0.92         7

    accuracy                           0.91        43
    macro avg       0.88      0.87      0.87        43
    weighted avg       0.92      0.91      0.91        43

    ```

    - with data augmentation
    
    ```bash
    0.7192982456140351


    [[6 0 0 0 0 1 0]
    [0 5 0 0 2 0 0]
    [1 0 3 0 0 0 0]
    [2 0 2 7 2 0 0]
    [1 1 0 0 6 0 0]
    [2 0 0 0 0 8 0]
    [0 1 0 0 0 1 6]]


                precision    recall  f1-score   support

            0       0.50      0.86      0.63         7
            1       0.71      0.71      0.71         7
            2       0.60      0.75      0.67         4
            3       1.00      0.54      0.70        13
            4       0.60      0.75      0.67         8
            5       0.80      0.80      0.80        10
            6       1.00      0.75      0.86         8

        accuracy                           0.72        57
    macro avg       0.74      0.74      0.72        57
    weighted avg       0.78      0.72      0.73        57
    ```
# Conclusion and TODO
- The augmentation data quality is not high, and the performance is not improved. At the same time, it also needs tuning and optimization.
- Since the inference speed is too slow, the expression parameter Loss needs to be reduced for longer trainning
  - batchsize
- Any good quantitative Loss/metric of the quality of the generated expression , now  L2 norm of landmark.
- Formualte  expression distribution for each expression, sample expression params from distribution instead of using fixed values. Here is a generative model.
- Use other network/method:
  - DeeperCNN
  - LBP+LP
  - IDA+SVM (sota in jaffe)
- Other dataset:
  - Fer2013(working on) 48*48 front grayscale
  - ExpW : in the wild / color
- Reprot(working on)