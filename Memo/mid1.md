# Expression linear
``` c++
case FaceExpression.Neutral:
    //                       4     1     2     5     7     6     9    10    17    15    25    26    27    16    20    12    23    24
    expressions = new List<float[]> {
        new float[nAUs] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f },
    };
    break;
case FaceExpression.Happiness:
    //                       4     1     2     5     7     6     9    10    17    15    25    26    27    16    20    12    23    24
    expressions = new List<float[]> {
        new float[nAUs] {    0,    0,    0,    0,    0,    0, 1.0f,    0,    0,    0,    0,    0,    0,    0,    0, 1.0f,    0,    0 },
        new float[nAUs] {    0,    0,    0,    0,    0,    0, 1.0f,    0,    0,    0,    0,    0,    0, 0.8f,    0, 1.0f,    0,    0 },
        new float[nAUs] {    0,    0,    0,    0,    0,    0, 1.0f,    0,    0,    0, 0.8f,    0,    0,    0,    0, 1.0f,    0,    0 },
        new float[nAUs] {    0,    0,    0,    0,    0,    0, 1.0f,    0,    0,    0,    0, 0.8f,    0,    0,    0, 1.0f,    0,    0 },
        new float[nAUs] {    0,    0,    0,    0,    0,    0, 1.0f,    0,    0,    0,    0,    0,    0,    0,    0, 1.0f,    0,    0 },
        new float[nAUs] {    0,    0,    0,    0,    0,    0, 1.0f,    0,    0,    0, 0.8f,    0,    0, 0.8f,    0, 1.0f,    0,    0 },
        new float[nAUs] {    0,    0,    0,    0,    0,    0, 1.0f,    0,    0,    0,    0, 0.8f,    0, 0.8f,    0, 1.0f,    0,    0 },
    };
    break;
case FaceExpression.Sadness:
    //                       4     1     2     5     7     6     9    10    17    15    25    26    27    16    20    12    23    24
    expressions = new List<float[]> {
        new float[nAUs] {    0, 1.0f,    0,    0,    0,    0,    0,    0, 1.0f, 1.0f,    0,    0,    0,    0,    0,    0,    0,    0 },
        new float[nAUs] { 0.8f, 1.0f,    0,    0,    0,    0,    0,    0, 1.0f, 1.0f,    0,    0,    0,    0,    0,    0,    0,    0 },
        new float[nAUs] {    0, 1.0f,    0,    0, 0.8f,    0,    0,    0, 1.0f, 1.0f,    0,    0,    0,    0,    0,    0,    0,    0 },
        new float[nAUs] {    0, 1.0f,    0,    0,    0,    0,    0,    0, 1.0f, 1.0f, 0.8f,    0,    0,    0,    0,    0,    0,    0 },
        new float[nAUs] {    0, 1.0f,    0,    0,    0,    0,    0,    0, 1.0f, 1.0f,    0, 0.8f,    0,    0,    0,    0,    0,    0 },
        new float[nAUs] { 0.8f, 1.0f,    0,    0,    0,    0,    0,    0, 1.0f, 1.0f, 0.8f,    0,    0,    0,    0,    0,    0,    0 },
        new float[nAUs] {    0, 1.0f,    0,    0, 0.8f,    0,    0,    0, 1.0f, 1.0f, 0.8f,    0,    0,    0,    0,    0,    0,    0 },
        new float[nAUs] { 0.8f, 1.0f,    0,    0,    0,    0,    0,    0, 1.0f, 1.0f,    0, 0.8f,    0,    0,    0,    0,    0,    0 },
        new float[nAUs] {    0, 1.0f,    0,    0, 0.8f,    0,    0,    0, 1.0f, 1.0f,    0, 0.8f,    0,    0,    0,    0,    0,    0 },
    };
    break;
case FaceExpression.Anger:
    //                       4     1     2     5     7     6     9    10    17    15    25    26    27    16    20    12    23    24
    expressions = new List<float[]> {
        new float[nAUs] { 1.0f,    0, 1.0f,    0, 1.0f,    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,    0, 1.0f, 1.0f },
        new float[nAUs] { 1.0f,    0, 1.0f,    0, 1.0f,    0,    0,    0, 0.8f,    0,    0,    0,    0,    0,    0,    0, 1.0f, 1.0f },
        new float[nAUs] { 1.0f,    0, 1.0f,    0, 1.0f,    0,    0,    0,    0,    0,    0,    0,    0, 0.8f,    0,    0, 1.0f, 1.0f },
        new float[nAUs] { 1.0f,    0, 1.0f,    0, 1.0f,    0,    0,    0,    0,    0, 0.8f,    0,    0,    0,    0,    0, 1.0f, 1.0f },
        new float[nAUs] { 1.0f,    0, 1.0f,    0, 1.0f,    0,    0,    0,    0,    0,    0, 0.8f,    0,    0,    0,    0, 1.0f, 1.0f },
        new float[nAUs] { 1.0f,    0, 1.0f,    0, 1.0f,    0,    0,    0, 0.8f,    0, 0.8f,    0,    0,    0,    0,    0, 1.0f, 1.0f },
        new float[nAUs] { 1.0f,    0, 1.0f,    0, 1.0f,    0,    0,    0,    0,    0, 0.8f,    0,    0, 0.8f,    0,    0, 1.0f, 1.0f },
        new float[nAUs] { 1.0f,    0, 1.0f,    0, 1.0f,    0,    0,    0, 0.8f,    0,    0, 0.8f,    0,    0,    0,    0, 1.0f, 1.0f },
        new float[nAUs] { 1.0f,    0, 1.0f,    0, 1.0f,    0,    0,    0,    0,    0,    0, 0.8f,    0, 0.8f,    0,    0, 1.0f, 1.0f },
    };
    break;
case FaceExpression.Disgust:
    //                       4     1     2     5     7     6     9    10    17    15    25    26    27    16    20    12    23    24
    expressions = new List<float[]> {
        new float[nAUs] {    0,    0,    0,    0,    0,    0, 1.0f, 1.0f,    0,    0,    0,    0,    0,    0,    0,    0,    0,    0 },
        new float[nAUs] {    0,    0,    0,    0,    0,    0, 1.0f, 1.0f, 0.8f,    0,    0,    0,    0,    0,    0,    0,    0,    0 },
        new float[nAUs] {    0,    0,    0,    0,    0,    0, 1.0f, 1.0f,    0,    0, 0.8f,    0,    0,    0,    0,    0,    0,    0 },
        new float[nAUs] {    0,    0,    0,    0,    0,    0, 1.0f, 1.0f,    0,    0,    0, 0.8f,    0,    0,    0,    0,    0,    0 },
        new float[nAUs] {    0,    0,    0,    0,    0,    0, 1.0f, 1.0f, 0.8f,    0, 0.8f,    0,    0,    0,    0,    0,    0,    0 },
        new float[nAUs] {    0,    0,    0,    0,    0,    0, 1.0f, 1.0f, 0.8f,    0,    0, 0.8f,    0,    0,    0,    0,    0,    0 },
    };
    break;
case FaceExpression.Fear:
    //                       4     1     2     5     7     6     9    10    17    15    25    26    27    16    20    12    23    24
    expressions = new List<float[]> {
        new float[nAUs] {    0, 1.0f,    0, 1.0f, 1.0f,    0,    0,    0,    0,    0,    0,    0,    0,    0, 1.0f,    0,    0,    0 },
        new float[nAUs] { 0.8f, 1.0f,    0, 1.0f, 1.0f,    0,    0,    0,    0,    0,    0,    0,    0,    0, 1.0f,    0,    0,    0 },
        new float[nAUs] {    0, 1.0f,    0, 1.0f, 1.0f,    0,    0,    0,    0,    0, 0.8f,    0,    0,    0, 1.0f,    0,    0,    0 },
        new float[nAUs] {    0, 1.0f,    0, 1.0f, 1.0f,    0,    0,    0,    0,    0,    0, 0.8f,    0,    0, 1.0f,    0,    0,    0 },
        new float[nAUs] { 0.8f, 1.0f,    0, 1.0f, 1.0f,    0,    0,    0,    0,    0, 0.8f,    0,    0,    0, 1.0f,    0,    0,    0 },
        new float[nAUs] { 0.8f, 1.0f,    0, 1.0f, 1.0f,    0,    0,    0,    0,    0,    0, 0.8f,    0,    0, 1.0f,    0,    0,    0 },
    };
    break;
case FaceExpression.Surprise:
    //                       4     1     2     5     7     6     9    10    17    15    25    26    27    16    20    12    23    24
    expressions = new List<float[]> {
        new float[nAUs] {    0, 1.0f, 1.0f, 1.0f,    0,    0,    0,    0,    0,    0,    0, 1.0f,   0f,    0,    0,    0,    0,    0 },
        new float[nAUs] {    0, 1.0f, 1.0f, 1.0f,    0,    0,    0,    0,    0,    0, 1.0f, 1.0f,   0f,    0,    0,    0,    0,    0 },
        new float[nAUs] {    0, 1.0f, 1.0f, 1.0f,    0,    0,    0,    0,    0,    0,    0, 1.0f, 0.6f,    0,    0,    0,    0,    0 },
    };
```
# What is blendshape?
- https://www.quora.com/What-is-blendshape-exactly

    BlendShape is Maya's implementation of the technique of allowing a single mesh to deform to achieve numerous pre-defined shapes and any number of combinations of in-between these shapes.

    This technique is very commonly used in facial rigs.
- 

# Obj file
``` python
# List of geometric vertices, with (x,y,z[,w]) coordinates, w is optional and defaults to 1.0. 
v 0.123 0.234 0.345 1.0 
v ... 
... 
# List of texture coordinates, in (u, v [,w]) coordinates, these will vary between 0 and 1, w is optional and defaults to 0. 
vt 0.500 1 [0] 
vt ... 
... 
# List of vertex normals in (x,y,z) form; normals might not be unit vectors. 
vn 0.707 0.000 0.707 
vn ... 
... 
# Parameter space vertices in ( u [,v] [,w] ) form; free form geometry statement ( see below ) 
vp 0.310000 3.210000 2.100000 
vp ... 
... 
# Polygonal face element (see below) 
f 1 2 3 
f 3/1 4/2 5/3 
f 6/4/1 3/5/3 7/6/5 
f 7//1 8//2 9//3 
f ... 
```
# Export shapekey (blendshape)
```python
# Export all Shape Keys as OBJs in Blender
# Version 1.0 - August 2017
# =========================================
# Original Script by Tlousky
# https://blender.stackexchange.com/questions/86674/how-to-batch-export-shapekeys-as-obj-from-the-active-object/86678#86678
# with small tweaks by Jay Versluis 
# https://www.versluis.com

import bpy
from os.path import join

# Reference the active object
o = bpy.context.active_object 

# CHANGE THIS to the folder you want to save your OBJ files in
# NOTE: no spaces, no trailing slash
exportPath = "/Users/you/somewhere" 

# Reset all shape keys to 0 (skipping the Basis shape on index 0
for skblock in o.data.shape_keys.key_blocks[1:]:
    skblock.value = 0

# Iterate over shape key blocks and save each as an OBJ file
for skblock in o.data.shape_keys.key_blocks[1:]:
    skblock.value = 1.0  # Set shape key value to max

    # Set OBJ file path and Export OBJ
    objFileName = skblock.name + ".obj" # File name = shapekey name
    objPath = join( exportPath, objFileName )
    bpy.ops.export_scene.obj( filepath = objPath, use_selection = True, global_scale = 1 )

    skblock.value = 0 # Reset shape key value to 0

# THE END
```
# Import Obj as shapekey (blendshape) Obj Sequence Importer
- [tutorial](https://blenderartists.org/t/addon-import-obj-sequence-as-shape-keys-on-one-object/575402)
- [script](https://blender.stackexchange.com/questions/58147/merging-multiple-obj-files-into-one-file-with-shape-keys)

``` python
bl_info = {
    'name': 'Load Obj Sequence as Shape Keys',
    'author': 'cmomoney',
    'version': (0, 2),
    'blender': (2, 6, 7),
    'category': 'Import-Export',
    'location': 'File > Import/Export',
    'wiki_url': ''}

# ##### BEGIN GPL LICENSE BLOCK #####
#
#  This program is free software; you can redistribute it and/or
#  modify it under the terms of the GNU General Public License
#  as published by the Free Software Foundation; either version 2
#  of the License, or (at your option) any later version.
#
#  This program is distributed in the hope that it will be useful,
#  but WITHOUT ANY WARRANTY; without even the implied warranty of
#  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
#  GNU General Public License for more details.
#
#  You should have received a copy of the GNU General Public License
#  along with this program; if not, write to the Free Software Foundation,
#  Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.
#
# ##### END GPL LICENSE BLOCK #####

import bpy, os
from bpy.props import *

class LoadObjAsShapekey(bpy.types.Operator):
    bl_idname = 'load.obj_as_shapekey'
    bl_label = 'Import OBJ as Shape Keys'
    bl_options = {'REGISTER', 'UNDO'}
    bl_description = "Import Obj sequence as shape key(s)"

    filepath = StringProperty(name="File path", description="File filepath of Obj", maxlen=4096, default="")
    filter_folder = BoolProperty(name="Filter folders", description="", default=True, options={'HIDDEN'})
    filter_glob = StringProperty(default="*.obj", options={'HIDDEN'})
    files = CollectionProperty(name='File path', type=bpy.types.OperatorFileListElement)
    filename_ext = '.obj'

    @classmethod
    def poll(cls, context):
        return context.active_object is not None and context.active_object.type == 'MESH'

    def execute(self, context):
        #get file names, sort, and set target mesh
        spath = os.path.split(self.filepath)
        files = [file.name for file in self.files]
        files.sort()
        target = bpy.context.scene.objects.active
        #add all ojs in sequence as shape  keys
        for f in files:
            fp = os.path.join(spath[0], f)
            self.load_obj(fp)
        #now delete objs
        sknames = [sk.name for sk in target.data.shape_keys.key_blocks]
        bpy.ops.object.select_all(action='DESELECT')
        for obj in sknames:
            if obj != 'Basis':
                target.data.shape_keys.key_blocks[obj].interpolation = 'KEY_LINEAR'
                bpy.context.scene.objects.active = bpy.data.objects[obj]
                bpy.data.objects[obj].select = True
                bpy.ops.object.delete()
            bpy.ops.object.select_all(action='DESELECT')
        #reselect target mesh and make active
        bpy.context.scene.objects.active = target
        target.select = True
        return{'FINISHED'}

    def invoke(self, context, event):
        wm = context.window_manager.fileselect_add(self)
        return {'RUNNING_MODAL'}

    def load_obj(self, fp):
        bpy.ops.import_scene.obj(filepath=fp,split_mode='OFF')
        bpy.ops.object.join_shapes()
        return

def menu_func_import(self, context):
    self.layout.operator(LoadObjAsShapekey.bl_idname, text="Obj As Shapekey(.obj)")

def register():
    bpy.utils.register_class(LoadObjAsShapekey)
    bpy.types.INFO_MT_file_import.append(menu_func_import)

def unregister():
    bpy.utils.unregister_class(LoadObjAsShapekey)
    bpy.types.INFO_MT_file_import.remove(menu_func_import)

if __name__ == "__main__":
    register()
```
# space-time nonlinear constranit
- 1h/iteration with nfaces: 22800     nverts: 11510
- 
# Q
- optimaziation slow 
- obj 