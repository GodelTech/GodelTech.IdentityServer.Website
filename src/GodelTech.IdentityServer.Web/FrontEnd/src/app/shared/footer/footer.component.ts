import { Component, OnInit } from "@angular/core";
import { AuthService } from "@core/auth/auth.service";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

@Component({
    // tslint:disable-next-line:component-selector
    selector: "app-footer",
    templateUrl: "./footer.component.html",
    styleUrls: ["./footer.component.scss"],
})
export class FooterComponent implements OnInit {
    private isAuthenticated$: Observable<boolean>;
    private userName$: Observable<string>;

    constructor(private authService: AuthService) {}

    ngOnInit() {
        this.userName$ = this.authService.getUserProfile().pipe(map((profile) => profile.userName));
        this.isAuthenticated$ = this.authService.isAuthenticated$;
    }
}
