export class AccessCheck {
  Id?: string;
  LicensePlate: string = '';
  CheckDateTime: Date = new Date();
  IsAccessGranted?: boolean;
  EmployeeId?: string;
}
