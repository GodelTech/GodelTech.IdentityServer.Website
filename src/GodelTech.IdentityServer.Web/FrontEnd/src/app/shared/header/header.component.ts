import { Component, OnInit } from "@angular/core";
import { AuthService } from "@core/auth/auth.service";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

@Component({
    selector: "app-header",
    templateUrl: "./header.component.html",
    styleUrls: ["./header.component.scss"],
})
export class HeaderComponent implements OnInit {
    public isExpanded: boolean;
    public isAuthenticated$: Observable<boolean>;
    public userName$: Observable<string>;

    constructor(private authService: AuthService) {
        this.isExpanded = false;
    }

    public ngOnInit() {
        this.collapse();

        this.userName$ = this.authService.getUserProfile().pipe(map((profile) => profile && profile.userName));
        this.isAuthenticated$ = this.authService.isAuthenticated$;
    }

    public async logout() {
        await this.authService.logout();
    }

    public collapse() {
        this.isExpanded = false;
    }

    public toggle() {
        this.isExpanded = !this.isExpanded;
    }
}
