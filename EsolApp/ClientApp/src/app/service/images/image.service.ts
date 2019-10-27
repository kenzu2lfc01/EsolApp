import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class ImageService {
  rootUrl = 'https://localhost:44346/api/Image';
  constructor(private http: HttpClient) { }

  postImage(formData) {
    const uploadReq = new HttpRequest('POST', this.rootUrl, formData, {
      reportProgress: true,
    });
    return uploadReq;
  }
  deleteImage(imageId) {
    return this.http.delete(this.rootUrl + '/delete/' + imageId);
  }
}
