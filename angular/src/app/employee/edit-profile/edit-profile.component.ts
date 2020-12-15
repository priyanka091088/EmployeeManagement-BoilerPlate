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
import { AppSessionService } from '@shared/session/app-session.service';
import { TokenService } from 'abp-ng2-module';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';

  class PagedRolesRequestDto extends PagedRequestDto {
    keyword: string;
  }
  @Component({
    templateUrl: 'edit-profile.component.html'
  })
  export class EditProfileComponent extends PagedListingComponentBase<EmployeeDTO>{
    saving = false;
    id:number;
    employee = new EmployeeDTO();
    emp:EmployeeDTO;
    employeeList:EmployeeDTO[];
    departList:DepartmentDTO[]=[];
    permissions: PermissionDto[] = [];
    checkedPermissionsMap: { [key: string]: boolean } = {};
    defaultPermissionCheckedStatus = true;
    keyword = '';
   
    @Output() onSave = new EventEmitter<any>();

    constructor(injector: Injector,private employeeService:EmployeeServiceProxy,private _tokenService: TokenService,
      private appservice:AppSessionService){
        super(injector);
    }

    list(
      request: PagedRolesRequestDto,
      pageNumber: number,
      finishedCallback: Function):void{
          request.keyword = this.keyword;

          let userdetail=this.appservice.user;
          this.employeeService
          .getAll(request.keyword, request.skipCount, request.maxResultCount)
          .pipe(
            finalize(() => {
              finishedCallback();
            })
          )
          .subscribe({
              next:res=>{
                this.employeeList = res.items;
                  console.log(this.employeeList)
                  this.emp=this.employeeList.find(e=>e.email==userdetail.emailAddress);
                  console.log(this.emp);
                  this.employeeService.get(this.emp.id).subscribe({
                    next:employee=>{
                      this.employee=employee;
                      console.log(this.employee);
                    }
                  })
              }
          });
  
      }
      delete(employee: EmployeeDTO): void {
          
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
            
            this.onSave.emit();
          });
      }
      }
  
  