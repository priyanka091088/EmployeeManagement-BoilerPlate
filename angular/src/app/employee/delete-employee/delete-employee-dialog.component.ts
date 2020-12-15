import {
    Component,
    Injector,
    OnInit,
    EventEmitter,
    Output,
  } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import { DepartmentDTO, DepartmentServiceProxy, EmployeeDTO, EmployeeServiceProxy, PermissionDto } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
  
  @Component({
    templateUrl: 'delete-employee-dialog.component.html'
  })
  export class DeleteEmployeeDialogComponent extends AppComponentBase implements OnInit{
    saving = false;
    id:number;
    employee = new EmployeeDTO();
    permissions: PermissionDto[] = [];
    checkedPermissionsMap: { [key: string]: boolean } = {};
    defaultPermissionCheckedStatus = true;
  
    @Output() onSave = new EventEmitter<any>();

    constructor(injector: Injector,
        private employeeService: EmployeeServiceProxy,
        public bsModalRef: BsModalRef){
        super(injector);
    }

      ngOnInit(): void {
        this.employeeService.get(this.id).subscribe(
            res=>{
                this.employee=res;
            }
        )
      }

      delete(): void {
        this.saving = true;
    
        const employee = new EmployeeDTO();
        employee.init(this.employee);
       
        this.employeeService
          .delete(employee.id)
          .pipe(
            finalize(() => {
              this.saving = false;
            })
          )
          .subscribe(() => {
            this.notify.success(this.l('Deleted Successfully'));
            this.bsModalRef.hide();
            this.onSave.emit();
          });
      }
      Back():void{
          this.bsModalRef.hide();
      }
}
  
  