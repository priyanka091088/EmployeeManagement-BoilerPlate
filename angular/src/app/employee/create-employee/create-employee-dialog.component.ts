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
import { DepartmentDTO, DepartmentServiceProxy, EmployeeDTO, EmployeeServiceProxy, PermissionDto, UserDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
  //import { AppComponentBase } from '@shared/app-component-base';

  class PagedRolesRequestDto extends PagedRequestDto {
    keyword: string;
  }
  @Component({
    templateUrl: 'create-employee-dialog.component.html'
  })
  export class CreateEmployeeDialogComponent extends PagedListingComponentBase<DepartmentDTO>{
    saving = false;
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
        private userService:UserServiceProxy,
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
          .create(employee)
          .pipe(
            finalize(() => {
              this.saving = false;
            })
          )
          .subscribe(() => {
            this.notify.info(this.l('Saved Successfully'));
            //this.addUser(employee);
            this.bsModalRef.hide();
            this.onSave.emit();
          });
      }
      addUser(employee:EmployeeDTO){
        const user=new UserDto();
        user.emailAddress=employee.email;
        
      }
      Back():void{
          this.bsModalRef.hide();
      }
}
  
  