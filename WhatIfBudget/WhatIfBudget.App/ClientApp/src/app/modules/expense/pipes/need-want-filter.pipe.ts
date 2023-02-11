import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'needWantFilter'
})
export class NeedWantFilterPipe implements PipeTransform {

  transform(items: any[], priority: number, field: string): any {
    return items.filter(item => item[field] == priority);
  }

}
