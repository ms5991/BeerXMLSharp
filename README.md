# BeerXMLSharp

[![Build Status](https://travis-ci.org/ms5991/BeerXMLSharp.svg?branch=master)](https://travis-ci.org/ms5991/BeerXMLSharp)

This is a C# [BeerXML](http://www.beerxml.com/beerxml.htm) object model and library for serializing/deserializing XML of this format.  It can be used in any .NET application that requires representation of Homebrewing recipies that conform to the open standard.

For the time being, as BeerXML V2 is not fully spec'ed out, this library only supports BeerXML V1. This will change if V2 gains traction.

## Usage

### Installation

Install the nuget package. In nuget, the package is called `BeerXMLSharp` and can be installed via the UI or `Install-Package BeerXMLSharp`.

### Object model and Serialization

Per the standard, every element in the BeerXML representation is either a "record" or a "record set".  As such, the object model defines several interfaces:

1. `BeerXMLSharp.OM.IBeerXMLEntity`: this is the highest level interface representing any entity that is a BeerXML element.  It defines methods:
    1. `string GetBeerXML()`: this method returns a string which contains the BeerXML representation of the BeerXML entity.
    2. `void GetBeerXML(string filePath)`: this method writes the BeerXML representing the BeerXML entity to a file at the given filePath.
    3. `void GetBeerXML(Stream stream)`: this method writes the BeerXML representing the BeerXML entity to the given `Stream` object.
    4. `bool IsValid()`: returns a `bool` indicating if the given BeerXML entity would result in a valid BeerXML representation per the BeerXML spec.
    5. `bool IsValid(out ValidationCode errorCode)`: does the same as (4) with the addition of an `out` parameter that contains the error code, if the method returns `false`.
2. `BeerXMLSharp.OM.IRecordSet`: this interface defines a "record set", which is a collection of records in the BeerXML spec.
    1. `void Add(IRecord child)`: adds the given `IRecord` to the record set.
    2. `void Remove(IRecord child)`: removes the given `IRecord` from the record set.
    3. `int Count`: gets the number of `IRecord`s in this record set.
3. `BeerXMLSharp.OM.IRecord`: this interface defines a record as defined in the BeerXML spec. It defines two common properties to all records, which are `Name` and `Version`.

The list of implemented `IRecordSet`s in the `BeerXMLSharp.OM.RecordSets` namespace includes:

* `Equipments`
* `Fermentables`
* `Hops`
* `Mash_Steps`
* `Mashs`
* `Miscs`
* `Recipes`
* `Styles`
* `Waters`
* `Yeasts`

The list of implemented `IRecord`s in the `BeerXMLSharp.OM.Records` namespace includes:

* `Equipment`
* `Fermentable`
* `Hop`
* `Mash_Step`
* `Mash`
* `Misc`
* `Recipe`
* `Style`
* `Water`
* `Yeast`

The descriptions and property lists for all `IBeerXMLEntity`s can be found in the [spec](http://www.beerxml.com/beerxml.htm).

### Deserialization

The static class `BeerXMLSharp.BeerXML` defines deserialization methods: 

1. `IBeerXMLEntity Deserialize(string filePath)`: deserializes an IBeerXMLEntity from the given file.
2. `IBeerXMLEntity Deserialize(Stream stream)`: deserializes an IBeerXMLEntity from the given `Stream`.

Further, `BeerXMLSharp.BeerXML` exposes a configuration property called `StrictModeEnabled`, which is a `bool` indicating whether a `BeerXMLUnknownTypeTagException` should be thrown if a `Deserialize` method encounters a tag with an unknown type. Note that the default for this property is `false` per the spec. When set to `true`, technically the program will be non-compliant because the spec requires that unknown tags be ignored. This property can be set as follows:

    BeerXML.StrictModeEnabled = true;
    BeerXML.StrictModeEnabled = false;

### BeerXML Exceptions

#### `BeerXMLInvalidObjectException`

This exception is thrown when attempting to serialize an `IBeerXMLEntity` for which any of the `IsValid()` methods would return `false`. This exception can be bypassed by setting `AllowInvalidSerialization` to `true` on an object prior to calling `GetBeerXML()`. This may be desirable in certain cases including when parsing XML from an external source.

#### `BeerXMLUnknownTypeTagException`

If strict mode is enabled, this exception is thrown during deserialization if the input XML contains an unknown tag that cannot be parsed into an `IRecord` or `IRecordSet` supported by BeerXML.

## Future

BeerXML V2+ if they are ever finalized, and [BeerJSON](https://old.reddit.com/r/Homebrewing/comments/7ej733/bregrammers_lets_work_on_beerjson_beerxml_2/) if it is more widely adopted.


