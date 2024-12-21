export class Envelope {
  Result?: any;
  Errors?: Error[];
  TimeCreated: Date;

  private constructor(result?: any, errors?: Error[]) {
    this.Result = result;
    this.Errors = errors;
    this.TimeCreated = new Date();
  }
}
