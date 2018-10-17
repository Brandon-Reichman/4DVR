# 4DVR

A Unity application for Oculus Rift+Touch to display projections of
surfaces in four-dimensional space, and to allow the user to apply 4D
rotations to such surfaces.

Initially developed in a Fall 2017 undergraduate research project of
the [Mathematical Computing Laboratory](http://mcl.math.uic.edu/) at
[UIC](http://uic.edu/).

## Status

The application can display the following surfaces:

* The 5-cell (4D simplex)
* The 8-cell (4D cube)
* RP^2 (with or without a narrow Mobius strip cut out)
* The flat torus in R^4

While these are the options available in the application, the
underlying code can load and display any triangular mesh in 4-space
stored in an OBJ file.  Note that OBJ files allow a fourth coordinate
for each vertex, which is typically silently ignored.  In this case,
the fourth coordinate is significant (and is read by our custom OBJ
parser).

The user can apply arbitrary rotations in a pair of orthogonal planes
(e.g. XY and ZW) using the hand controllers.

## Controls

The control scheme is as follows:

Escape: Quit application.

WASD or Arrow Keys: Move and rotate camera.

Spacebar: Move camera up.

Left Control: Move camera down.

Shift: Cycles through planes of rotation.

Tab & Backslash: Cycle through objects.

Z: Switches to orthographic projection.

X: Switches to stereographic projection.

Middle-Click: Resets object orientation.

### Rotations

To rotate the object, click and hold either mouse button, depending on rotation desired. Where initially clicked will define the origin point. Draw circles around the origin point. The difference in the angle drawn around the origion will be the angle the object is rotated on the selected plane.

Left-Click: Four-dimensional rotations.

Right-Click: Three-dimentional rotations.


## Known issues

* 4D rotations and lighting do not interact properly, because normals
  are not correctly calculated after applying 4D rotations in the
  custom shader.

* The two sides of each triangle in the 4D object's mesh are shaded
  slightly differently, resulting in an apparent "seam" in the
  non-orientable surfaces.

* Need to add visualization for rotating object with mouse controls.

* The control scheme is not documented in a way that can be accessed
  from within the VR experience.

## Requirements

* [Unity 3D](http://unity3d.com/version) version 2017.1
* Oculus Rift
* Oculus Touch

## Development Team

* Brandon Reichman (<Reichman.Brandon@gmail.com>)
* David Dumas (<david@dumas.io>)
