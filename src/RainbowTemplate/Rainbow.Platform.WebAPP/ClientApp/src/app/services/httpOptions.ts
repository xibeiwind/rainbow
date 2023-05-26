import { HttpHeaders } from '@angular/common/http';

export function getHttpOptions() {
  return {
    headers: new HttpHeaders({
      Authorization: `Bearer ${localStorage["token"]}`
    })
  };
}
