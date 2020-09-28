This week I first spent a few days to solve the order of the vertices in obj. According to the suggestions from Tom and Xiaoxiao, I consulted some model designers. Then search for related issues on the forum, and successfully realized the linear combination of blendshape by modifying some others' scripts. 

Due to computer limitations, I currently only show one model. 

Thank to tom. I have downloaded the facewarehouse dataset and spent some time on this dataset.
The resolution is about 10k vectics. There are 20 expressions,46 + 1 neutral blendshape for 150 person. 

Another problem is that I compared some photos of FACS from website with blendshape, and they still look different. I re-read the facewarehouse paper, but unfortunately, it didn't mention how the original blendshape was constructed. just say 46FACS system.

After that, I want to calculate the weight of blendshape corresponding to different poses so that we can reconstruct expressions.
But there is a problem here. Even for similar looks, different people have different weights for similar expression. Here it is likely that everyone's neutral face looks different. I didn't check it. 
In fact, when making this data set, there should be some problems. Some people with the same neutral face have their eyes closed and others have their eyes open.

I also spent some time creating a docker image so that we can reproduce the experiment at any time later without having to configure the environment multiple times. 

I carefully studied the previous blendshape transfer formula and code, and modified part of the code so that it can run underconstraint. Unfortunately. Because it is underconstraint, this linear system cannot guarantee optimal, so the final result is that there is a problem with the vertex position calculation, and a large number of broken surfaces appear.

And very lucky, When I re-read the survey paper about 3dmm before, I read an article and I found an article from Max planke insititution. They are also building a system similar to FaceX, called SMPL-X which is A new joint 3D model of the human body, face and hands together. they release the a sub-project call Flame, which is a 3d morphable model a few week ago. I also got these model a few days ago.

You can control these model parameters at will to achieve different faces and expressions.


So now there are three way for the next work.

The first is to build our own female, male and general models based on the facewarehouse based on FLame's article ideas and codes. The second is to expand on the basis of flame. We divide the model into regions by feature points and map by regions. Or think of other ways for different ages and skins.



<!-- The traditional method is to divide the face into different areas and map them separately. The advantage is that the materials of different areas can be changed separately, such as changing the texture materials of the eyebrows, lips, and eyes, so as to achieve different ages and different appearances. Determine if these areas must be divided manually.

It is difficult to control the texture of different areas of the face separately. It is necessary to use some style transfer techniques to first generate face images of different ages, and then generate the corresponding uv texture. -->


