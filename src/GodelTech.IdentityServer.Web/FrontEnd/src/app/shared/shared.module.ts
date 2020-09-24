import { CommonModule } from "@angular/common";
import { ModuleWithProviders, NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { LoaderComponent } from "./loader/loader.component";
import { HeaderComponent } from "./header/header.component";
import { FooterComponent } from "./footer/footer.component";

@NgModule({
    imports: [CommonModule, FormsModule, ReactiveFormsModule, RouterModule],
    providers: [],
    declarations: [LoaderComponent, HeaderComponent, FooterComponent],
    exports: [LoaderComponent, HeaderComponent, FooterComponent],
})
export class SharedModule {
    static forRoot(): ModuleWithProviders<SharedModule> {
        return {
            ngModule: SharedModule,
        };
    }
}
