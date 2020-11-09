# Papers
- [3D Morphable Face Models - Past, Present and Future](https://hal.inria.fr/hal-02280281/document)
- [Rigid Stabilization of Facial Expressions](https://studios.disneyresearch.com/wp-content/uploads/2019/03/Rigid-Stabilization-of-Facial-Expressions.pdf)
- [Learning a model of facial shape and expression from 4D scans](https://ringnet.is.tue.mpg.de/)
- [Learning to Regress 3D Face Shape and Expression from an Image without 3D Supervision](https://ps.is.mpg.de/publications/ringnet-cvpr-2019)

# FLAME
- [FLAME: Faces Learned with an Articulated Model and Expressions](https://github.com/Rubikplayer/flame-fitting)

> FLAME is a lightweight and expressive generic head model learned from over 33,000 of accurately aligned 3D scans.
> FLAME can e.g. be used to synthesize new motion sequences, by transferring the facial expression from a source actor to a target actor
Our FLAME model (Faces Learned with an Articulated Model andExpressions) is factored in that it separates the representation ofidentity, pose, and facial expression, similar to models of the hu-man body [Anguelov et al.2005; Loper et al.2015]. To keep themodel simple, computationally efficient, and compatible with exist-ing game and rendering engines, we define a vertex-based modelwith a relatively low polygon count, articulation, and blend skin-ning. Specifically FLAME includes a learned shape space of identityvariations, an articulated jaw and neck, and eyeballs that rotate.Additionally we learn pose-dependent blendshapes for the jaw andneck from examples. Finally, we learn “expression” blendshapes tocapture non-rigid deformations of the face

# Super 3d mesh

# Related Project
- [Perceiving Systems Mesh Package](https://github.com/MPI-IS/mesh)
> This package contains core functions for **manipulating meshes and visualizing them**.

# Meeting
- [] build our own female , male and general models based on the facewarehouse based on FLame's  ideas and codes. 
- [] Extend flame. 
  - We divide the model into regions by feature points and map by regions. 
  - Or other ways for different ages and skin uv mapping
- [x] Aus transfer, build model form image.