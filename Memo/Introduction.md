# Week1

## FACS : Facial Action Coding System

* Basic emotions: anger, disgust, fear, happiness, sadness, surprise, contempt, and neutral
* Expression
  * Each AU is identified by a number (AU1, AU2, AU4...)  and  correspond  to  the  activation  of  a  single  facial  muscle
  * Intensities of FACS are expressed by letters from A (minimalintensity) to E (maximal intensity) 

## Emotion recognition

Feature extractor (G)

? what if mix emotions? mask -> emotion label -> mask (arbitrary)

build a generative network? (cool thing?)

## Previous work

### 3D model

* Naturalfront [http://naturalfront.com/] 
  * [https://assetstore.unity.com/packages/tools/animation/naturalfront-3d-face-animation-plugin-free-48380]
  * 3D modeling and animation
  * Plugin for Unity
  
* OpenFACS : an open source FACS-based 3D face animation system [https://github.com/phuselab/openFACS]
  * [https://www.youtube.com/watch?v=fzMYU-9qYaw]
  * any improve?
  
* OpenFace 2.2.0: a facial behavior analysis toolkit [https://github.com/TadasBaltrusaitis/OpenFace/]
  * facial landmark detection
  * head pose estimation
  * **facial action unit recognition**
  * eye-gaze estimation
  
* FACSHuman: a software that allows researchers to create, through three-dimensional modeling, experimental material that can beused in nonverbal communication and emotional facial expressionsresearches. [https://github.com/montybot/FACSHuman]
  * just front?

### GAN

* GANimation: Anatomically-aware FacialAnimation from a Single Image
  * Attention-based generator
  * attention mask / color transformation (Generative Netwrok??)
  
* StarGAN: Unified Generative Adversarial Networksfor Multi-Domain Image-to-Image Translation
  learning mappings among multiple do-mains using a single generator
  * <u>Hair</u>
  * <u>Gender</u>
  * <u>Aged</u>
  * Skin
  * Emotions
  
* Dataset: RaFD,KDEF,FER
  * RaFD:Each model was trained to show the following expressions: Anger, disgust, fear, happiness, sadness, surprise, contempt, and neutral. Each emotion was shown with three different gaze directions and all pictures were taken from five camera angles simultaneously.

## Future Work

* Tools Experiments

* ? style transfer?
* ? Face-to-Paramete (3D  face  reconstruction)
  * Image to 3D
* Scheme
  * Typical 3D model -> Typical Au Control

## Meeting Memo

Label distribution learning
Xiaoxiao Sun对所有人说： 10:56 AM
Xin geng

## Advice

* unity emotions
* ~~appearance~~
* PersonX can be used for qualitative analysis, the functions of certain models under different lighting and occlusion conditions
* FaceX might fixed emtion first and control material

## Next week

* Learn Unity
* Learn GAN to genrate different material
