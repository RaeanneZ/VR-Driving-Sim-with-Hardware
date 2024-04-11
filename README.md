# VR Driving Simulator with Unity
## Background
Driving simulators are in growing demands as they offers a safe and controlled environment for novice drivers to learn and gain experience, as well as for professional drivers to enhance their skills. A virtual reality driving simulator further enhances the immersive experience for the users. Additionally, the lightweight setup is  a more cost effective and portable solution.

## Objective
The goal of the simulator is to teach driving theories and road awareness in a visual way through scenarios. The simulator provides a realistic, experiential and immersive driving experience, without being physically on the road. Learners will also have the opportunity to practice defensive driving in a safe and controlled environment. This prototype is an experimentation and exploration of unity 3D.

## Target Audience
1. 18 years old and above
2. Does not have a car to practice driving in real life
3. Busy work-life and prefers learning during their own free time
4. Wants to practice road awareness from the comfort of their own home 
5. Varying degrees of driving experience, including those without basic theories

##  Development Platform
The driving simulator uses Unity development game engine. Unity provides 2D and 3D platforms to create video games. Unity uses C# language for programming and is chosen because of its wide community, resources, has a free personal license and supports games that incorporate Virtual Reality (VR). 
To create a lifelike environment fast, Google Map Tiles API is used in conjunction with Cesium to procedurally generate the world map. Cesium is an open-source software that helps to combine its 3D geospace capability and 3D tiles with Unity. Google Map Tiles API provides high resolution photorealistic 3D tiles which are used to generate the environment. 

##  Features Implemented
1. 3D environment (Singapore settings) built with Cesium Google Map Platform Photorealistic 3D Tiles and Blender
2. Immersive driving experience with VR Headset - Oculus Quest 2
3. Realistic vehicle controls with steering wheel and accelerator pedals. 
4. CGF car on road to simulate traffic conditions
5. Simulating weather conditions 

## Files
**Car Controls** 

UserCarControls.cs

**CGF Vehichle (the NPC Cars)**

vehicleAIController.cs (for car movement), Waypoint.cs (to create waypoints and its attributes), carNode.cs (child of Waypoint.cs, it is specifically for vehicle node only), generateWaypoint.cs and WaypointManagerWindow.cs (to draw the route that cars will follow) 

*Note: To use generateWaypoint.cs and WaypointManagerWindow.cs, under Toolbar in Unity, hover for dropdown and select, it should open as a window in Unity*

**Traffic Light**

TrafficLight.cs (this is just to change traffic light color between red, amber and green), TrafficLightController.cs (to control multiple traffic lights at once for areas like cross junction, all timing syncronisation should be done here)


## Resources Used 
#### Car AI
1. [Traffic System In Unity Video](https://www.youtube.com/watch?v=MXCZ-n5VyJc)
   
#### Calling Oculus Controllers’ Inputs
1. [Oculus Quest 2 Controller Buttons Names](https://www.youtube.com/watch?v=OhZlCqHOosw)
2. [Unity Forum To Get A-B-X-Y Buttons](https://forum.unity.com/threads/oculus-quest-how-to-detect-a-b-x-y-button-presses.1108232/)
   
#### Connecting to External Devices
1. [Enabling Action Map in Script to Read Input Values](https://stackoverflow.com/questions/69193146/unity-new-input-system-returning-0-or-not-working)
2. [Input Controls Document](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Controls.html)
3. [Set up Input Actions in Unity](https://www.youtube.com/watch?v=Yjee_e4fICc)

#### Driving Simulator Implementation
1. [Measure Car Velocity](https://forum.unity.com/threads/measuring-car-speed.531772/)
2. [Mirror Tutorial Video](https://www.youtube.com/watch?v=txF4t1qynyk)
3. [Turn Steering Wheel in VR Video](https://www.youtube.com/watch?v=w1f1Q8vDr_g)
4. [Unity Quaternion Explanation Video](https://www.youtube.com/watch?v=RQHG_Tv9vzA)
5. [Wheel Collider Explanation Video](https://www.youtube.com/watch?v=c-yhZwXSx_c)
   
#### Importing Google Map Tiles API into Unity with Cesium
1. [Set up Cesium in Unity Video](https://www.youtube.com/watch?v=lLw5hCqSv5Y)

#### Traffic Light Implementation
1. [Explanation on Time.deltaTime Video](https://www.youtube.com/watch?v=8pYq15Lh0x4)
2. [How Traffic Signals Work](https://www.youtube.com/watch?v=DP62ogEZgkI)
3. [Singapore's RAG Arrows Rationale](https://www.youtube.com/watch?v=w8j3XGaxkrA)
4. [Unity Forum on Getting GameObject Material](https://discussions.unity.com/t/how-can-i-assign-materials-using-c-code/2205/4)
