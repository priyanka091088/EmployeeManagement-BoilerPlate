import { Component, ElementRef, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateRoleDialogComponent } from '@app/roles/create-role/create-role-dialog.component';
import { EditRoleDialogComponent } from '@app/roles/edit-role/edit-role-dialog.component';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/paged-listing-component-base';
import { DepartmentDTO, DepartmentServiceProxy, EmployeeDTO, EmployeeServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppSessionService } from '@shared/session/app-session.service';
import { LocalizationService, PermissionCheckerService, FeatureCheckerService, NotifyService, SettingService, MessageService, AbpMultiTenancyService } from 'abp-ng2-module';
import { request } from 'https';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { CreateEmployeeDialogComponent } from './create-employee/create-employee-dialog.component';
import { EditEmployeeDialogComponent } from './edit-employee/edit-employee-dialog.component';
class PagedRolesRequestDto extends PagedRequestDto {
    keyword: string;
  }
@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  
})
export class EmployeeComponent extends PagedListingComponentBase<EmployeeDTO>{
  public pageTitle:string="Employee List";
  employeeList: EmployeeDTO[] = [];
  employeeDetails:EmployeeDTO[];
  keyword = '';

  public departNameList:string[]=[];
  constructor(injector: Injector,private departmentService:DepartmentServiceProxy,
    private _modalService: BsModalService,private employeeService:EmployeeServiceProxy)
     {
         super(injector);
     }
  list(
    request: PagedRolesRequestDto,
    pageNumber: number,
    finishedCallback: Function):void{
        request.keyword = this.keyword;
        
        this.employeeService
        .getAll(request.keyword, request.skipCount, request.maxResultCount)
        .pipe(
          finalize(() => {
            finishedCallback();
          })
        )
        .subscribe({
            next:res=>{
              var a=0;
              
                this.employeeList = res.items;
                console.log(this.employeeList)
                for(var j=0;j<this.employeeList.length;j++){
                  console.log(this.employeeList[j].departId)
                    this.departmentService.get(this.employeeList[j].departId).subscribe({
                        next:dep=>{
                          console.log(dep.departName)
                          this.departNameList[a]= dep.departName;
                          a++;
                        }
                    })
                }
                console.log(this.departNameList);
                
                this.showPaging(res, pageNumber);
            }
        });

    }
    delete(employee: EmployeeDTO): void {
        abp.message.confirm(
          this.l('RoleDeleteWarningMessage', employee.name),
          undefined,
          (result: boolean) => {
            if (result) {
              this.employeeService
                .delete(employee.id)
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
      createEmployee(): void {
        this.showCreateOrEditemployeeDialog();
      }
    
      editEmployee(employee: EmployeeDTO): void {
        this.showCreateOrEditemployeeDialog(employee.id);
      }
    
      showCreateOrEditemployeeDialog(id?: number): void {
        let createOrEditemployeeDialog: BsModalRef;
        if (!id) {
            createOrEditemployeeDialog = this._modalService.show(
              CreateEmployeeDialogComponent,
            {
              class: 'modal-lg',
            }
          );
        } else {
            createOrEditemployeeDialog = this._modalService.show(
              EditEmployeeDialogComponent,
            {
              class: 'modal-lg',
              initialState: {
                id: id,
              },
            }
          );
        }
    
        createOrEditemployeeDialog.content.onSave.subscribe(() => {
          this.refresh();
        });
      }

}
