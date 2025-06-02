# Physics-Simulator
[WORK IN PROGRESS] The C# files needed to simulate a 2D momentum simulator in Unity

## Center of Mass
To find the center of mass of the system, you have to find the center's x location and the center's y location. To find these two points, separetly add all of ball's x and y components together and divide these two sums by the number of circles.

![center_of_mass](https://github.com/user-attachments/assets/55ccc8f7-45ee-4bc2-8ed9-af20387d8e3e)


## Collision Detection
A collision occurs when the distance between two circles is less than or equal to the sum of the radii of both circles.

![collision_detection](https://github.com/user-attachments/assets/5d139262-cf2a-4d76-8db6-3d4f541e1c7a)

## Derivation of final velocities
To understand an elastic collision in 2D, we first have to understand the derivation of elastic collisions in 1D.

### 1D Elastic Collisions
