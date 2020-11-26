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
  //import { AppComponentBase } from '@shared/app-component-base';
  
  @Component({
    templateUrl: 'edit-department-dialog.component.html'
  })
  export class EditDepartmentDialogComponent extends AppComponentBase implements OnInit{
    saving = false;
    id:number;
    depart = new DepartmentDTO();
    permissions: PermissionDto[] = [];
    checkedPermissionsMap: { [key: string]: boolean } = {};
    defaultPermissionCheckedStatus = true;
  
    @Output() onSave = new EventEmitter<any>();

    constructor(injector: Injector,
        private departmentService: DepartmentServiceProxy,
        public bsModalRef: BsModalRef,
        private router:Router){
        super(injector);
    }

      ngOnInit(): void {
          
         this.departmentService.get(this.id).subscribe(
             res=>{
                 this.depart=res;
             }
         )
      }

      edit(): void {
        this.saving = true;
    
        const depart = new DepartmentDTO();
        depart.init(this.depart);
        //depart.grantedPermissions = this.getCheckedPermissions();
    
        this.departmentService
          .update(depart)
          .pipe(
            finalize(() => {
              this.saving = false;
            })
          )
          .subscribe(() => {
            this.notify.info(this.l('updated Successfully'));
            this.bsModalRef.hide();
            this.onSave.emit();
          });
      }
      Back():void{
          this.bsModalRef.hide();
      }
}
  
  