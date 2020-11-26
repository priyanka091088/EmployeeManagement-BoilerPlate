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
    templateUrl: 'create-depatment-dialog.component.html'
  })
  export class CreateDepartmentDialogComponent extends AppComponentBase implements OnInit{
    saving = false;
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
         
      }

      save(): void {
        this.saving = true;
    
        const depart = new DepartmentDTO();
        depart.init(this.depart);
        //depart.grantedPermissions = this.getCheckedPermissions();
    
        this.departmentService
          .create(depart)
          .pipe(
            finalize(() => {
              this.saving = false;
            })
          )
          .subscribe(() => {
            this.notify.info(this.l('Saved Successfully'));
            this.bsModalRef.hide();
            this.onSave.emit();
          });
      }
      Back():void{
          this.bsModalRef.hide();
      }
}
  
  