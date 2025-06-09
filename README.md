# Physics-Simulator
This repositor includes the C# files needed to simulate a 2D momentum simulator in Unity

Here is a video of a visual demonstration: https://youtu.be/tXe5whtTRaU

## Center of Mass
To find the center of mass of the system, you have to find the center's x location and the center's y location. To find these two points, separetly add all of ball's x and y components together and divide these two sums by the number of circles.

![center_of_mass](https://github.com/user-attachments/assets/55ccc8f7-45ee-4bc2-8ed9-af20387d8e3e)


## Collision Detection
A collision occurs when the distance between two circles is less than or equal to the sum of the radii of both circles.

![collision_detection](https://github.com/user-attachments/assets/5d139262-cf2a-4d76-8db6-3d4f541e1c7a)

## Derivation of final velocities
To understand an elastic collision in 2D, we first have to understand the derivation of elastic collisions in 1D.

### 1D Elastic Collisions
![1d_elastic](https://github.com/user-attachments/assets/34366d45-134b-44b7-a88d-e776bf964da0)

### 2D Elastic Collisions
You can simplify a 2D elastic collisions by rotating the collision so it is in one dimension. Then you rotate the collision back.

![2d_elastic](https://github.com/user-attachments/assets/b27b38eb-7833-440e-a725-91f5475e07ec)

For a better, and more detailed derivation, please visit William Craver's article (https://williamecraver.wixsite.com/elastic-equations).


## Detecting if two objects heading towards eachother
In some collisions, the two objects may not faze out of eachother before the next frame, therefore we have to check if the two objects are heading towards eachother. If they are, it is a collision. Else, it is not a collision. To do this, make a matrix of the distance difference between the two object's coordinates and a matrix of the difference in velocity between the two object's coordinates. Find the dot product between these two matrixes. The objects are heading toward eachother when at least on of the coordinates results in a negative number.

![closer](https://github.com/user-attachments/assets/75c424ce-8eaf-4066-9240-38f32e2c4846)

