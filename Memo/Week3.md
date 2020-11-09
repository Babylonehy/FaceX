# 3D facial expresion Recognition 

- [3D Approaches and Challenges in Facial ExpressionRecognition Algorithms—A Literature Review](https://www.mdpi.com/2076-3417/9/18/3904/pdf)

In recent years, facial expression analysis and recognition (FER) have emerged as an activeresearch topic with applications in several different areas, including the human-computer interactiondomain. Solutions based on 2D models are not entirely satisfactory for real-world applications, as theypresent some problems of pose variations and illumination related to the nature of the data. Thanks to technological development, 3D facial data, both still images and video sequences, have become increasingly used to improve the accuracy of FER systems. Despite the advance in 3D algorithms,these solutions still have some drawbacks that make pure three-dimensional techniques convenient only for a set of specific applications; a viable solution to overcome such limitations is adopting a multimodal 2D+3D analysis. In this paper, we analyze the limits and strengths of traditional and deep-learning FER techniques, intending to provide the research community an overview of theresults obtained looking to the next future. Furthermore, we describe in detail the most used databases to address the problem of facial expressions and emotions, highlighting the results obtained bythe various authors. The different techniques used are compared, and some conclusions are drawnconcerning the best recognition rates achieved.

- [Deep Facial Expression Recognition: A Survey](https://arxiv.org/pdf/1804.08348)

- [Learning from Millions of 3D Scans for Large-scale 3D Face Recognition](https://openaccess.thecvf.com/content_cvpr_2018/papers/Gilani_Learning_From_Millions_CVPR_2018_paper.pdf)

- [聚焦人脸表情识别 (FER) 的顶级会议和期刊文献与资源列表](https://bbs.cvmart.net/articles/395/zi-yuan-ju-jiao-ren-lian-biao-qing-shi-bie-fer-de-ding-ji-hui-yi-he-qi-kan-wen-xian-yu-zi-yuan-lie-biao#1)

- [Deep Neural Network Augmentation: Generating Faces for AffectAnalysis](https://link.springer.com/article/10.1007/s11263-020-01304-3)

This paper presents a novel approach for synthesizing facial affect; either in terms of the six basic expressions (i.e., anger, disgust, fear, joy, sadness and surprise), or in terms of valence (i.e., how positive or negative is an emotion) and arousal (i.e., power of the emotion activation). The proposed approach accepts the following inputs:(i) a neutral 2D image of a person; (ii) a basic facial expression or a pair of valence-arousal (VA) emotional state descriptors to be generated, or a path of affect in the 2D VA space to be generated as an image sequence. In order to synthesize affect in terms of VA, for this person, 600,000 frames from the 4DFAB database were annotated. The affect synthesis is implemented by fitting a 3D Morphable Model on the neutral image, then deforming the reconstructed face and adding the inputted affect, and blending the new face with the given affect into the original image. Qualitative experiments illustrate the generation of realistic images, when the neutral image is sampled from fifteen well known lab-controlled or in-the-wild databases, including Aff-Wild, AffectNet, RAF-DB; comparisons with generative adversarial networks (GANs) show the higher quality achieved by the proposed approach. Then, quantitative experiments are conducted, in which the synthesized images are used for data augmentation in training deep neural networks to perform affect recognition over all databases; greatly improved performances are achieved when compared with state-of-the-art methods, as well as with GAN-based data augmentation, in all cases.

# DataSet

- [Analyzing Facial Expressions and Emotions in Three Dimensional Space with Multimodal Sensing](https://www.cs.binghamton.edu/~lijun/Research/3DFE/3DFE_Analysis.html)

 3D facial models have been extensively used for 3D face recognition and 3D face animation, the usefulness of such data for 3D facial expression recognition is unknown. To foster the research in this field, we created a 3D facial expression database (called BU-3DFE database), which includes 100 subjects with 2500 facial expression models. The BU-3DFE database is available to the research community (e.g., areas of interest come from as diverse as affective computing, computer vision, human computer interaction, security, biomedicine, law-enforcement, and psychology.)

The database presently contains 100 subjects (56% female, 44% male), ranging age from 18 years to 70 years old, with a variety of ethnic/racial ancestries, including White, Black, East-Asian, Middle-east Asian, Indian, and Hispanic Latino. Participants in face scans include undergraduates, graduates and faculty from our institute’s departments of Psychology, Arts, and Engineering (Computer Science, Electrical Engineering, System Science, and Mechanical Engineering). The majority of participants were undergraduates from the Psychology Department (collaborator: Dr. Peter Gerhardstein).


Each subject performed seven expressions in front of the 3D face scanner. With the exception of the neutral expression, each of the six prototypic expressions (happiness, disgust, fear, angry, surprise and sadness) includes four levels of intensity. Therefore, there are 25 instant 3D expression models for each subject, resulting in a total of 2,500 3D facial expression models in the database. Associated with each expression shape model, is a corresponding facial texture image captured at two views (about +45° and -45°). As a result, the database consists of 2,500 two-view’s texture images and 2,500 geometric shape models.

- [4D facial database (4DFAB)] 
  
# Paper with code

- https://paperswithcode.com/task/facial-expression-recognition
- https://paperswithcode.com/task/3d-facial-expression-recognition
- [Expression-Net](https://github.com/fengju514/Expression-Net)

# Unity Mesh

- [如何在Unity中编辑网格/顶点](https://www.thinbug.com/q/32733293)
- [Editing mesh vertices in Unity](https://answers.unity.com/questions/14567/editing-mesh-vertices-in-unity.html)
- [unity 编辑mesh顶点位置](https://blog.csdn.net/zgjllf1011/article/details/79305756)
- [Runtime Mesh Manipulation With Unity](https://www.raywenderlich.com/3169311-runtime-mesh-manipulation-with-unity)

# Future Works

- Identity Aus
  - 3D facial landmark
  - Group Aus
  - [**3-Dimensional facial expression recognitionin human using multi-points warping**](FaceX\Reference\3D Au Recognition\3-Dimensional facial expression recognitionin human using multi-points warping.pdf)
  
- Manipulate Aus
  - Decompile Naturalfront to see how to manipulate Aus
  
- Expression transfer 
  - [*All-In-One: Facial Expression Transfer, Editing and Recognition Using A Single Network*](https://arxiv.org/abs/1911.07050)
  
  In this paper, we present a unified architecture known as Transfer-Editing and Recognition Generative Adversarial Network (TER-GAN) which can be used: 1. to transfer facial expressions from one identity to another identity, known as Facial Expression Transfer (FET), 2. to transform the expression of a given image to a target expression, while preserving the identity of the image, known as Facial Expression Editing (FEE), and 3. to recognize the facial expression of a face image, known as Facial Expression Recognition (FER). In TER-GAN, we combine the capabilities of generative models to generate synthetic images, while learning important information about the input images during the reconstruction process. More specifically, two encoders are used in TER-GAN to encode identity and expression information from two input images, and a synthetic expression image is generated by the decoder part of TER-GAN. To improve the feature disentanglement and extraction process, we also introduce a novel expression consistency loss and an identity consistency loss which exploit extra expression and identity information from generated images. Experimental results show that the proposed method can be used for efficient facial expression transfer, facial expression editing and facial expression recognition. In order to evaluate the proposed technique and to compare our results with state-of-the-art methods, we have used the Oulu-CASIA dataset for our experiments.

  - [Unconstrained Facial Expression Transfer using Style-based Generator]()

# Advice

- Aus group
- Degree of control
- Mesh to Aus
