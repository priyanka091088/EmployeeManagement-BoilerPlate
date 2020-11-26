import { Component, ElementRef, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateRoleDialogComponent } from '@app/roles/create-role/create-role-dialog.component';
import { EditRoleDialogComponent } from '@app/roles/edit-role/edit-role-dialog.component';
import { PagedListingComponentBase, PagedRequestDto, PagedResultDto } from '@shared/paged-listing-component-base';
import { DepartmentDTO, DepartmentServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppSessionService } from '@shared/session/app-session.service';
import { LocalizationService, PermissionCheckerService, FeatureCheckerService, NotifyService, SettingService, MessageService, AbpMultiTenancyService } from 'abp-ng2-module';
import { request } from 'https';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { CreateDepartmentDialogComponent } from './create-department/create-depatment-dialog.component';
import { EditDepartmentDialogComponent } from './edit-department/edit-department-dialog.component';

class PagedRolesRequestDto extends PagedRequestDto {
    keyword: string;
  }
@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  
})
export class DepartmentComponent extends PagedListingComponentBase<DepartmentDTO>{
  public pageTitle:string="Department List";
  departList:DepartmentDTO[];
  depart: DepartmentDTO[] = [];
  keyword = '';
  constructor(injector: Injector,private departmentService:DepartmentServiceProxy,private _modalService: BsModalService) {super(injector);}
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
      createDepartment(): void {
        this.showCreateOrEditDepartDialog();
      }
    
      editDepartment(depart: DepartmentDTO): void {
        this.showCreateOrEditDepartDialog(depart.id);
      }
    
      showCreateOrEditDepartDialog(id?: number): void {
        let createOrEditDepartDialog: BsModalRef;
        if (!id) {
            createOrEditDepartDialog = this._modalService.show(
                CreateDepartmentDialogComponent,
            {
              class: 'modal-lg',
            }
          );
        } else {
            createOrEditDepartDialog = this._modalService.show(
              EditDepartmentDialogComponent,
            {
              class: 'modal-lg',
              initialState: {
                id: id,
              },
            }
          );
        }
    
        createOrEditDepartDialog.content.onSave.subscribe(() => {
          this.refresh();
        });
      }

}
