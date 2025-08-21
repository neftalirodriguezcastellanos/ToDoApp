import type { AlertColor } from "@mui/material";

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
  fullName: string;
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

export interface Task {
  id: string;
  title: string;
  description?: string;
  dueDate?: Date;
  createdAt: Date;
  modifiedAt: Date;
  isCompleted: boolean;
  color?: string;
}

export type TaskFormData = {
  id?: string;
  title?: string;
  description?: string;
  dueDate?: string;
  color?: string;
  isCompleted?: boolean;
};

export interface AlertData {
  message: string;
  severity?: AlertColor; // "error" | "warning" | "info" | "success"
}
