# String Conversions
What is this for? To convert a single line string into an object!

### Installation
To add it to your Unity project. Add this url to your `manifest.json`:
```json
"com.popcron.string-conversions": "https://github.com/popcron/string-conversions.git"
```

### Example
```cs
string value = "(0.2, 0.1, -5)";
Vector3 vector = value.Convert<Vector3>();
vector.x = int.MaxValue;
value = Conversion.ToString(vector);
```

### Adding new converters
It is possible to add a new convert by simply inheriting from the `Converter<>` type and implementing the abstract methods.
