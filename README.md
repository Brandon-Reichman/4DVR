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

## Documentation

The control scheme is as follows:

A Button: Cycles through planes of rotation.
B Button: Resets objects orientation.
X Button: Switches to orthographic projection.
Y Button: Switches to stereographic projection.
Left Menu Button: Quits application.
Right Menu Button: Open Oculus menu.
Left/Right Index Trigger: Cycles through objects.
Right Grip Trigger: Three-dimensional rotations. (Twist right wrist as if grabbing and turning a dial)
Left Grip Trigger: Four-dimensional rotations. (Twist left wrist as if grabbing and turning a dial)


## Known issues

* 4D rotations and lighting do not interact properly, because normals
  are not correctly calculated after applying 4D rotations in the
  custom shader.

* The two sides of each triangle in the 4D object's mesh are shaded
  slightly differently, resulting in an apparent "seam" in the
  non-orientable surfaces.

* The control scheme is not documented in a way that can be accessed
  from within the VR experience.

## Requirements

* [Unity 3D](http://unity3d.com/version) version 2017.1
* Oculus Rift
* Oculus Touch

## Development Team

* Brandon Reichman (<Reichman.Brandon@gmail.com>)
* David Dumas (<david@dumas.io>)
