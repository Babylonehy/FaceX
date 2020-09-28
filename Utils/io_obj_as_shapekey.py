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
            fp = spath[0] + "\\" + f
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
        bpy.ops.import_scene.obj(filepath=fp)
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