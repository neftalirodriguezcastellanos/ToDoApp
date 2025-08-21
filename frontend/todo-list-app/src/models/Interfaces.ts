export interface ResponseGeneric<T> {
  data: T | null;
  isSuccess: boolean;
  message: string;
}

export type TokenData = {
  token: string;
  expires: Date;
};

export interface LoginResponse {
  email: string;
  dataAuth: TokenData;
  roles: string[];
}

export type ApiError = {
  type: string;
  title: string;
  status: number;
  errors: {
    [key: string]: string[];
  };
  traceId: string;
};
