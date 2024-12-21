import {HttpInterceptorFn} from '@angular/common/http';

export const baseUrlInterceptor: HttpInterceptorFn = (req, next) => {
  const apiUrl = 'http://localhost:5170/api'; // Базовый URL

  // Проверяем, является ли URL запроса абсолютным
  if (req.url.startsWith('http://') || req.url.startsWith('https://')) {
    return next(req); // Не модифицируем абсолютные запросы
  }

  // Добавляем базовый URL к относительным запросам
  const apiReq = req.clone({
    url: `${apiUrl}/${req.url}`,
  });

  console.log('Intercepted request with URL:', apiReq.url);

  return next(apiReq);
};
