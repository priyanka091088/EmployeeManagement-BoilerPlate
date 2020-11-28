import { Component, ElementRef, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/paged-listing-component-base';
import { DepartmentDTO, DepartmentServiceProxy, EmployeeDTO, EmployeeServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppSessionService } from '@shared/session/app-session.service';
import { LocalizationService, PermissionCheckerService, FeatureCheckerService, NotifyService, SettingService, MessageService, AbpMultiTenancyService } from 'abp-ng2-module';
import { request } from 'https';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
class PagedRolesRequestDto extends PagedRequestDto {
    keyword: string;
  }
@Component({
  selector: 'view-employee',
  templateUrl: './view-employee-of-sameDepart.component.html',
  
})
export class ViewEmployeeOfSameDepartComponent extends PagedListingComponentBase<EmployeeDTO>{
  public pageTitle:string="Employee List";
  employeeList: EmployeeDTO[] = [];
  employeeDetails:EmployeeDTO[];
  keyword = '';

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
        .getAllEmployees()
        .pipe(
          finalize(() => {
            finishedCallback();
          })
        )
        .subscribe({
            next:res=>{
              
                this.employeeList = res;
                console.log(this.employeeList);
            }
        });

    }
    delete(employee: EmployeeDTO): void {
      }
      

}
