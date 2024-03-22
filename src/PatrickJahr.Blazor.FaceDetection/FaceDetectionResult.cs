using System.Text.Json.Serialization;

namespace PatrickJahr.Blazor.FaceDetection;

public record BoundingBox(
    [property: JsonPropertyName("x")] double? X,
    [property: JsonPropertyName("y")] double? Y,
    [property: JsonPropertyName("width")] double? Width,
    [property: JsonPropertyName("height")] double? Height,
    [property: JsonPropertyName("top")] double? Top,
    [property: JsonPropertyName("right")] double? Right,
    [property: JsonPropertyName("bottom")] double? Bottom,
    [property: JsonPropertyName("left")] double? Left
);

public record Landmark(
    [property: JsonPropertyName("locations")] IReadOnlyList<Location> Locations,
    [property: JsonPropertyName("type")] string Type
);

public record Location(
    [property: JsonPropertyName("x")] double? X,
    [property: JsonPropertyName("y")] double? Y
);

public record FaceDetectionResult(
    [property: JsonPropertyName("boundingBox")] BoundingBox BoundingBox,
    [property: JsonPropertyName("landmarks")] IReadOnlyList<Landmark> Landmarks
);