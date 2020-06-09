# Object-Pooling
 A look at what Object Pooling is and how it can impact performance

Object pooling is the concept of storing a given amount of objects in a dictionary, allowing for the objects to be reused. This is useful in that it can prevent from the endless instantiation of objects.

# How it Works
There are three key components to getting this to work: There is a dictionary that accepts a string for the key and a queue of game objects for the corresponding value. There is also a list of all the possible pools.

Magic happens within our SpawnFromPool method inside of Object pooler. This method accepts a string, a vector3 for position, and a quaternion for rotation. The string is used to index into the dictionary, dequeuing and storing what is found into a temporary objectToSpawn variable. this object is set to active and given the position and rotation from the parameters. The object is then requeued.

# To do
- Make something practical
- Allow GUI/interface to customize on the fly
