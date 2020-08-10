# Week2

## [3D Basel Face Model (BFM)](https://faces.dmi.unibas.ch/bfm/bfm2019.html)

    - Shape and Texture
    - 

## [Unsupervised Learning of Probably Symmetric Deformable 3D Objectsfrom Images in the Wild](https://github.com/elliottwu/unsup3d)

We propose a method to learn weakly symmetric deformable 3D object categories from raw single-view images, without ground-truth 3D, multiple views, 2D/3D keypoints, prior shape models or any other supervision.

## [Neural 3D Mesh Renderer](https://hiroharu-kato.com/projects_en/neural_renderer.html) 

For modeling the 3D world behind 2D images, which 3D representation is most appropriate? A polygon mesh is a promising candidate for its compactness and geometric properties. However, it is not straightforward to model a polygon mesh from 2D images using neural networks because the conversion from a mesh to an image, or rendering, involves a discrete operation called **rasterization**, which prevents back-propagation. Therefore, in this work, we propose **an approximate gradient for rasterization** that enables the integration of rendering into neural networks. Using this renderer, we perform single-image 3D mesh reconstruction with silhouette image supervision and our system outperforms the existing voxel-based approach. Additionally, we perform gradient-based 3D mesh editing operations, such as 2D-to-3D style transfer and 3D DeepDream, with 2D supervision for the first time. These applications demonstrate the potential of the integration of a mesh renderer into neural networks and the effectiveness of our proposed renderer.

## [ShapeNet](https://www.shapenet.org/)

ShapeNet is an ongoing effort to establish a richly-annotated, large-scale dataset of 3D shapes. We provide researchers around the world with this data to enable research in computer graphics, computer vision, robotics, and other related disciplines. ShapeNet is a collaborative effort between researchers at Princeton, Stanford and TTIC.

## [Describable Textures Dataset (DTD)](https://www.robots.ox.ac.uk/~vgg/data/dtd/)

DTD is a texture database, consisting of 5640 images, organized according to a list of 47 terms (categories) inspired from human perception. There are 120 images for each category. Image sizes range between 300x300 and 640x640, and the images contain at least 90% of the surface representing the category attribute. The images were collected from Google and Flickr by entering our proposed attributes and related terms as search queries. The images were annotated using Amazon Mechanical Turk in several iterations. For each image we provide key attribute (main category) and a list of joint attributes.

## *Baseline*

