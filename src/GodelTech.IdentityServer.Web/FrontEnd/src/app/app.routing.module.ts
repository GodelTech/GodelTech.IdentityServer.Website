import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { PagesModule } from "./pages/pages.module";

export const routes = [
    { path: "", redirectTo: "login", pathMatch: "full" },
    { path: "**", redirectTo: "not-found" },
];

@NgModule({
    imports: [RouterModule.forRoot(routes), PagesModule],
    declarations: [],
    exports: [RouterModule],
})
export class RoutesModule {}
