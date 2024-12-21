namespace Domain.Utils;

public static class Errors
{
    public static class General
    {
        public static Error NotOwner(Guid? id = null)
        {
            var Id = id == null ? "Id" : $"{id}";
            return Error.Forbidden("not.owner", $"You are not the owner of {Id}");
        }

        public static Error AccessDenied(string? resource = null)
        {
            var label = resource ?? "resource";
            return Error.Validation($"{label}.access.denied", $"Access to {label} is denied");
        }

        public static Error AlreadyExist(string? name = null)
        {
            var label = name ?? "entity";
            return Error.Validation($"{label}.already.exist", $"{label} already exist");
        }

        public static Error AlreadyUsed(Guid? id = null)
        {
            var Id = id == null ? "Id" : $"{id}";
            return Error.Conflict("value.already.used", $"{Id} is already used. Operation impossible");
        }

        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation("value.is.invalid", $"{label} is invalid");
        }

        public static Error NotFound(Guid? id = null, string? objectName = null)
        {
            var forId = id == null ? "" : $" for Id '{id}'";
            var label = objectName == null ? "record" : $"{objectName}";
            return Error.NotFound("record.not.found", $"{label} not found {forId}");
        }

        public static Error ValueIsRequired(string? name = null)
        {
            var label = name == null ? "" : " " + name + " ";
            return Error.Validation("length.is.invalid", $"invalid {label} length");
        }

        public static Error LicensePlateIsEmpty()
        {
            return Error.Validation("license.plate.empty", "Attempted to add a car with an empty license plate.");
        }
    }
}
