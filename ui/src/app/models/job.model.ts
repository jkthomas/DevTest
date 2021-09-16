import { CustomerModel } from "./customer.model";

export interface JobModel {
  jobId: number;
  engineer: string;
  customer: CustomerModel;
  when: Date;
}
