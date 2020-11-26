import {
    Component,
    Injector,
    OnInit,
    EventEmitter,
    Output,
  } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { DepartmentDTO, DepartmentServiceProxy, EmployeeDTO, EmployeeServiceProxy, PermissionDto } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';

  class PagedRolesRequestDto extends PagedRequestDto {
    keyword: string;
  }
  @Component({
    templateUrl: 'edit-employee-dialog.component.html'
  })
  export class EditEmployeeDialogComponent extends PagedListingComponentBase<DepartmentDTO>{
    saving = false;
    id:number;
    employee = new EmployeeDTO();
    departList:DepartmentDTO[]=[];
    permissions: PermissionDto[] = [];
    checkedPermissionsMap: { [key: string]: boolean } = {};
    defaultPermissionCheckedStatus = true;
    keyword = '';
    @Output() onSave = new EventEmitter<any>();

    constructor(injector: Injector,
        private departmentService: DepartmentServiceProxy,
        private employeeService:EmployeeServiceProxy,
        public bsModalRef: BsModalRef){
        super(injector);
    }
    list(
        request: PagedRolesRequestDto,
        pageNumber: number,
        finishedCallback: Function):void{
            request.keyword = this.keyword;
    
            this.departmentService
            .getAll(request.keyword, request.skipCount, request.maxResultCount)
            .pipe(
              finalize(() => {
                finishedCallback();
              })
            )
            .subscribe({
                next:res=>{
                    this.departList = res.items;
                    console.log(this.departList);
                    this.showPaging(res, pageNumber);
                }
            });
            this.employeeService.get(this.id).subscribe(
                res=>{
                    this.employee=res;
                }
            )
        }

    delete(depart: DepartmentDTO): void {
        abp.message.confirm(
          this.l('RoleDeleteWarningMessage', depart.departName),
          undefined,
          (result: boolean) => {
            if (result) {
              this.departmentService
                .delete(depart.id)
                .pipe(
                  finalize(() => {
                    abp.notify.success(this.l('Successfully Deleted'));
                    this.refresh();
                  })
                )
                .subscribe(() => {});
            }
          }
        );
      }
      
      save(): void {
        this.saving = true;
    
        const employee = new EmployeeDTO();
        employee.init(this.employee);
    
        this.employeeService
          .update(employee)
          .pipe(
            finalize(() => {
              this.saving = false;
            })
          )
          .subscribe(() => {
            this.notify.info(this.l('Updated Successfully'));
            this.bsModalRef.hide();
            this.onSave.emit();
          });
      }
      Back():void{
          this.bsModalRef.hide();
      }
}
  
  