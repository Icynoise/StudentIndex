import { HttpParams } from '@angular/common/http';

/**
 * Parametri za SmartResult endpointe.
 * - search: quick search po svim tekstualnim kolonama
 * - sort:   npr. 'naziv|dsc,ects'
 * - page / pageSize: paginacija (bez njih server vraća sve)
 * - filters: filteri po kolonama, npr. { naziv: 'cn|matem', ects: 'ge|6' }
 */
export interface QueryOptions {
  search?: string;
  sort?: string;
  page?: number;
  pageSize?: number;
  returnNumberOfRows?: boolean;
  filters?: Record<string, string>;
}

export function buildQueryParams(options?: QueryOptions, base?: HttpParams): HttpParams {
  let params = base ?? new HttpParams();
  if (!options) return params;

  if (options.search) params = params.set('search', options.search);
  if (options.sort) params = params.set('sort', options.sort);
  if (options.page != null) params = params.set('page', options.page);
  if (options.pageSize != null) params = params.set('pageSize', options.pageSize);
  if (options.returnNumberOfRows) params = params.set('returnNumberOfRows', true);

  for (const [column, value] of Object.entries(options.filters ?? {})) {
    params = params.set(column, value);
  }

  return params;
}
