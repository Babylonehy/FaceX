# Blendshape

- [带蒙皮的网格渲染器 (Skinned Mesh Renderer)](https://docs.unity3d.com/cn/current/Manual/class-SkinnedMeshRenderer.html)
- [Unity FaceSync](https://github.com/joscanper/unity_facesync)
- [Blend Shape Builder & Vertex Tweaker](https://github.com/unity3d-jp/BlendShapeBuilder)
- [VoxBlend](https://github.com/hiroki-o/VoxBlend)

# Paper

- FEAFA: A Well-Annotated Dataset for Facial Expression Analysis and 3D FacialAnimation 
- FaceWarehouse: a 3D Facial Expression Databasefor Visual Computing
    - http://kunzhou.net/zjugaps/facewarehouse/
    - A dataset for blendshape
    - Code
      - [An Evaluator for Extreme 3D Face Reconstruction with Facewarehouse ground truth](https://github.com/FrancescoGradi/extreme_3d_faces_evaluator) 
      - [Deep3DFaceReconstruction](https://github.com/microsoft/Deep3DFaceReconstruction)
      - [3DDFA](https://github.com/cleardusk/3DDFA)
      - 
- SliderGAN: Synthesizing Expressive Face Images by Sliding 3D Blendshape Parameters 

## Candidate Papers
- Blend Shape Interpolation and FACS for Realistic Avatar
- A Preliminary Investigation into the Impact of Training forExample-Based Facial Blendshape Creation
- FaceScape: a Large-scale High Quality 3D Face Dataset andDetailed Riggable 3D Face Prediction

# Tutorial 
- [Setting up Blendshapes in Unity](https://learn.unity.com/tutorial/setting-up-blendshapes-in-unity)
- [Working with blend shapes](https://docs.unity3d.com/2020.1/Documentation/Manual/BlendShapes.html)
- [polywink](https://www.polywink.com/)
- [assigning shape keys by scripting python](https://blenderartists.org/t/assigning-shape-keys-by-scripting-python/406489/6)
  
# Aus Detection
- [PAttNet: Patch-attentive deep networkfor action unit detection]
- 
- [AU_R-CNN](https://github.com/machanic/AU_R-CNN)
- [Action Unit Detection](https://biomedicalcomputervision.uniandes.edu.co/index.php/research?id=30)
- [Automated Facial Affect Recognition (AFAR)](https://github.com/AffectAnalysisGroup/AFARtoolbox)

  Automated measurement of face and head dynamics, detection of facial action units and expression, and affect detection are crucial to multiple domains (e.g., health, education, entertainment). Commercial tools are available but costly and of unknown validity. Open-source ones lack user-friendly GUI for use by non-programmers. For both types, evidence of domain transfer and options for retraining for use in new domains typically are lacking. Deep approaches have two key advantages. They typically outperform shallow ones for facial affect recognition. And pre-trained models provided by deep approaches can be fine tuned with new datasets to optimize performance. AFAR is an open-source, deep-learning based, user-friendly tool for automated facial affect recognition. It consists of a pipeline having four components: (i) face tracking, ii) face registration, (iii) action unit (AU) detection and (iv) visualization. Moreover, finetuning component allows the interested users to finetune the pretrained AU detection models with their own dataset. AFAR has been used in comparative studies of action unit detectors [1], [2] and to investigate cross-domain generalizability [3], assess treatment response to deep brain stimulation (DBS) for treatment-resistant obsessive compulsive disorder [4], and to explore facial dynamics in young children [5] and in adults in treatment for depression [6] among other research.
- [FACS_recognition](https://github.com/jdlamstein/happybot)

  The problem I aimed to solve was: Can I make a script to identify AUs and emotions? I used the dlib library in Python to track facial features in the CK+ dataset. Following Tian's paper, the script calculated key distances such as distance between the eyebrows, distance between the lips, and the distance between the corner of the lips and the eyes. With OpenCV, I used the Canny edge detection on the corners of the eyes and between the brows to find the deepening of lines and furrows. Lines and furrows are a transient feature. Adults will have more wrinkles than children and, alone, they are not a reliable indicator of AUs or emotions. However, for this dataset, adding the transient features increased accuracy.

  The neural network was written in tensorflow and summarized in tensorboard. I was amused by the time it took me to concatenate a matrix, but I was pleased by how fit the module is for machine learning.

- [**Facial Action Unit Intensity Estimation via Semantic Correspondence Learningwith Dynamic Graph Convolution**]()
- [Expression transfer: A system to build 3D blend shapes for facial animation]()
- [Blendshape Facial Animation]()
  ## Survey
  - [AWESOME-FER](https://github.com/EvelynFan/AWESOME-FER)
  - [3D Facial Expression Synthesis: A Survey]()
# Meeting
Traditional pipeline:

RGBD(Color image, Depth map) -(ASM,feature points extract)-> feature points (internal, contour) -(morphable model,*with depth map*??)-> inital mesh -(refined algorithm, Laplacian-based mesh deformation algrithm)-> refined mesh -(*deformation transfer algorithm*)-> other experssions mesh (refined) -(example-based facial rigging algorithm)-> linear blendershap model -(PCA)-> Blinear(identities and expressions) Face model

1. Done
   - [x] Group Aus manually (18 Aus)
      - ![Aus](../images/Aus.png)
      - ![Face Aus](../images/Aus—definition.png)
   - [x] Blendshapes manipulation
     - [ ] whether or not use that dataset include blendshapes, such as *Facewarehouse*. 
   - [x] Linear Blendshape Model
   - [x] Get Aus's postions 
   - [x] Test model via Unity
2. Q
   1. How to search for a specifical topic paper or method?
   2. Any idea of 2D to 3D mapping?
   3. FEAFA or *Facewarehouse*？BU-3DFE 
3. Todo
   - [ ] Blendshape transfer 
   - [ ] Test **SCC** work or not
     - [ ] If work，try to group Aus in 3D model (cool thing)
       - [ ] How to achieve mapping?
   - [ ] Baseline System Part 1
     - [ ] image pre-processing
     - [ ] feature representation
     - [ ] Aus detection
     - [ ] Aus blendshapes
   