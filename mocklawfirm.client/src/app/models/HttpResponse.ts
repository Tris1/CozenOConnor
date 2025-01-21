import { HttpResponseBase } from "@angular/common/http";

export class HttpResponse extends HttpResponseBase{
  accessToken?: string;
}
