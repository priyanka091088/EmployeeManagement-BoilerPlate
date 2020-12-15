import {
    Component,
    Injector,
    OnInit,
    EventEmitter,
    Output,
  } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponentBase } from '@shared/app-component-base';
import { DepartmentDTO, DepartmentServiceProxy, PermissionDto } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
  
  @Component({
    templateUrl: 'delete-department.component.html'
  })
  export class DeleteDepartmentDialogComponent extends AppComponentBase implements OnInit{
    saving = false;
    id:number;
    depart = new DepartmentDTO();
    permissions: PermissionDto[] = [];
    checkedPermissionsMap: { [key: string]: boolean } = {};
    defaultPermissionCheckedStatus = true;
  
    @Output() onSave = new EventEmitter<any>();

    constructor(injector: Injector,
        private departmentService: DepartmentServiceProxy,
        public bsModalRef: BsModalRef){
        super(injector);
    }

      ngOnInit(): void {
        this.departmentService.get(this.id).subscribe(
            res=>{
                this.depart=res;
            }
        )
      }

      delete(): void {
        this.saving = true;
    
        const depart = new DepartmentDTO();
        depart.init(this.depart);
       
        this.departmentService
          .delete(depart.id)
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
  
  