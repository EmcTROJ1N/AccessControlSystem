export class Error {
  private static readonly SEPARATOR = "||";
  code: string;
  message: string;
  type: ErrorType;
  invalidField?: string;

  private constructor(code: string, message: string, type: ErrorType, invalidField?: string) {
    this.code = code;
    this.message = message;
    this.type = type;
    this.invalidField = invalidField;
  }
}

export enum ErrorType
{
  Validation,

  NotFound,
  Conflict,
  Forbidden,
  Failure
}
