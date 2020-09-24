import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";

import { Error404Component } from "./error404/error404.component";
import { Error500Component } from "./error500/error500.component";
import { LoginCallbackComponent } from "./login-callback/login-callback.component";
import { LoginComponent } from "./login/login.component";
import { ForgotPasswordComponent } from "./forgot-password/forgot-password.component";
import { ProfileComponent } from "./profile/profile.component";
import { ResetPasswordComponent } from "./reset-password/reset-password.component";
import { RegisterComponent } from "./register/register.component";
import { SharedModule } from "../shared/shared.module";
import { HomeComponent } from "./home/home.component";

const routes: Routes = [
    { path: "", redirectTo: "home", pathMatch: "full" },
    { path: "home", component: HomeComponent },
    { path: "login", component: LoginComponent },
    { path: "login-callback", component: LoginCallbackComponent },
    { path: "not-found", component: Error404Component },
    { path: "500", component: Error500Component, data: { title: "Error" } },
    { path: "register", component: RegisterComponent, pathMatch: "full" },
    { path: "forgot-password", component: ForgotPasswordComponent, pathMatch: "full" },
    { path: "profile", component: ProfileComponent, pathMatch: "full" },
    { path: "reset-password", component: ResetPasswordComponent, pathMatch: "full" },
];

@NgModule({
    imports: [CommonModule, SharedModule, FormsModule, ReactiveFormsModule, RouterModule.forChild(routes)],
    declarations: [
        LoginComponent,
        Error404Component,
        LoginCallbackComponent,
        Error500Component,
        RegisterComponent,
        ForgotPasswordComponent,
        ProfileComponent,
        ResetPasswordComponent,
        HomeComponent,
    ],
    exports: [RouterModule],
})
export class PagesModule {}
