# Creating interactive stellar system display using marker-based augmented reality

The goal of our project is to implement interactions on and between virtual objects using markers. For this purpose, we base ourselves on the article "Immersive Authoring of Tangible Augmented Reality Applications" by Gun A. Lee. We implement this within Unity using the ArucoUnity framework.

We make use of this system to represent a layout of the Solar System using markers to represent the background, the planets as well as satellites. We also provide ways to interact and transform the displayed objects. This could be used for educative purposes in order to teach about basic astronomy.

## Authors

Brisset Remi, Pepin Teiki & Peruzzetto Enzo

## Usage

This is a Unity project which can be imported as is and includes all of its dependencies.
This project heavily uses [the existing work](https://github.com/NormandErwan/ArucoUnity) by Erwan Normand which implements functions to create and detect Aruco markers within Unity using OpenCV.

You will need printed Aruco markers to do anything within the program. The ones used by default are the markers whose id are 0 to 12 and 21 to 23. The markers used can be changed within Unity in order to use different Aruco markers if needed.

For obvious reasons, you will also need a video acquisition system in order to acquire your markers and display the AR scene on top of them.

## Background

A default texture is assigned to a plane in Unity. This is used in order to represent the background of the scene in such a way that planets are hovering above the displayed image.

![Background](/Images/background.png)

This background is set to be displayed when and only when two specific markers, Aruco markers 0 and 1 by default, are visible in the video frame. Once every half second, it is moved to the middle point between these two markers and then its scale is modified in order to stretch from one marker to the other. This allows us to display a nicely sized scene whether the two markers are perfectly aligned or scattered.

The background image's position is only updated twice per second in order to keep it displayed in cases where the camera loses track of the markers for a split second. This is something that we have found to happen quite regularly when a camera is often handheld such as in this kind of project.

```
passedTime += Time.deltaTime;
if (passedTime > UPDATE_TIME) {
    setPosition(background, marker0, marker1);
    background.transform.localScale = new Vector3(dist * 0.1f, dist * 0.1f, dist * 0.1f);
    passedTime = 0.0f;
}
```

It is also worth noting that in order to actually have the scene be aligned with the way the planets are displayed, its rotation is set to one of the two markers. This allows us to keep the background parallel to the markers.

## Planets

Available in the Unity assets for this projects are provided textures for each planet and their corresponding spherical representation. Each of these are assigned to GameObjects within our Unity scene and, in a similar manner to the background, we display a planet whenever its corresponding marker (2 to 10 by default) appears within the view.

![Planets](/Images/planets.png)

These planet objects are initially parentless within Unity but are then assigned as a child to their corresponding parent once the script in charge of their display is initialized. This and some additional transformations on the object allow us to keep the planet moving according to the movement of its marker. As such, moving or rotating the marker also does the same action on the displayed planet.

```
void setParent(GameObject planet, GameObject marker)
{
    planet.transform.parent = marker.transform;
    planet.transform.localPosition = new Vector3 (.0f, 0.05f, .0f);
}

void setPosition(GameObject planet, GameObject marker, float rot)
{
    planet.transform.position = marker.transform.position;
    planet.transform.rotation = Quaternion.Euler(0, rot, 0);
}
```

We also use a time-based detection in order to reduce the amount of display stutter on our planet by assuming that losing sight of the marker for a very short time should still display the model at its previous position.

A planet can be assigned what is essentially an other planet as a child within Unity in order to have it behave like a moon. The parent planet's rotation will also apply a rotation on the child objects. A basic rotation is also applied to every tracked planet in order to simulate a geocentric rotation.

![Earth](/Images/earth.png)

## Marker Transfer

Any tracked marker that is not associated with a specific planet (id 11, 12 and 22 by default) behaves like a possible receiver for planet transfer. This can be done interactively by moving a marker with a planet on it adjacent to one of the empty markers. The planet and its current transform parameters will be moved over to the empty marker and the former planet-holding marker now becomes an empty marker. 

In order to avoid constant planet exchange at every update when markers are close by, we have set a cooldown of two seconds on transfers. This provides enough time to move a marker close to another, observe its transfer on the screen and move the markers apart.

```
With i being the index of the receiving marker and j the index of a potential sender :

for (int j = 0; j < markers.Count; j++) {
    if (planets[j] != null && transferCooldown == .0f && distance(markers[i], markers[j]) < TRANSFER_DIST_MIN) {
        planets[i] = planets[j];
        planets[j] = null;
        setParent(planets[i], markers[i]);
        transferCooldown = COOLDOWN_VALUE;
        break;
    }
}
```

![Transfer1](/Images/transfer1.png)
![Transfer2](/Images/transfer2.png)
![Transfer3](/Images/transfer3.png)

## Rescaling

Three additional markers (21-23 by default) are used in order to change the scale of the planets. This is done by setting 21 and 23 on opposite end from each other with 22 in the middle. Since 22 is also an empty marker as defined in the previous section, it can take any planet from another marker by having them close to each other.

Once the planet is displayed above this central marker, its size can be altered by moving the marker closer to either end. Moving it closer to the marker 21 will make the planet smaller in size (along with any moon it has), whereas 23 will make it grow larger. It is important to note that leaving this central marker in either of these location will keep progressively decreasing/increasing its size until this marker is moved elsewhere or sent back to regular scene markers.

![Slider1](/Images/sliderMin.png)
![Slider1](/Images/sliderMax.png)

A handy feature that is implemented with this method is that the closer the central marker is to one of the boundary markers, the quicker it will change in size. In this way, being adjacent to the 23 marker will quickly make the planet much bigger than what is probably desired.

```
Vector3 init = child.localScale;
float dist = distance(min, max);
float dist2 = distance(min, middle);
float d = dist2/dist;
Vector3 s = new Vector3(d*2.0f, d*2.0f, d*2.0f);
child.localScale = Vector3.Scale(init, s);
```

## Improvements

There is no doubt that many further improvements can be made to this project. Here is a short list of some of them :

* Improve continuous marker detection
* Add heliocentric rotations
* Custom planet models
* Improve the layout of our objects within Unity
* Collisions detection
* Accurate scales and orbits
* Planet selector marker in order to choose which planet a marker takes in the video feed
* Interplanetary satellites transfer
* Add audio
