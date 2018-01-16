import {ModuleWithProviders, NgModule} from '@angular/core';
import {AuthService} from '../../services/auth.service';

@NgModule({
    imports: [],
    declarations: [],
    exports: []
})
export class AuthModule {
    public static forRoot(): ModuleWithProviders {
        return {
            ngModule: AuthModule,
            providers: [
                AuthService
            ]
        };
    }
}
