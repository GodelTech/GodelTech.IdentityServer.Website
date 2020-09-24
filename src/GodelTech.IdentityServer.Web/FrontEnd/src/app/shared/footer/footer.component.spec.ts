import { TestBed, async, inject } from "@angular/core/testing";
import { FooterComponent } from "./footer.component";
import { AuthService } from "@core/auth/auth.service";

describe("Component: Footer", () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [],
            providers: [],
        }).compileComponents();
    });

    it("should create an instance", async(
        inject([AuthService], (authService) => {
            const component = new FooterComponent(authService);
            expect(component).toBeTruthy();
        }),
    ));
});
