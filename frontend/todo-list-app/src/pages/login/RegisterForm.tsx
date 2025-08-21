import { useForm } from "react-hook-form";
import { Box, TextField, Button, Alert, CircularProgress } from "@mui/material";

type RegisterFormData = {
  fullName: string;
  email: string;
  password: string;
};

interface RegisterFormProps {
  onRegister: (data: RegisterFormData) => void;
  loading?: boolean;
  errorMessage?: string;
}

const RegisterForm = ({
  onRegister,
  loading,
  errorMessage,
}: RegisterFormProps) => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterFormData>({
    mode: "onBlur",
  });

  const onSubmit = (data: RegisterFormData) => onRegister(data);

  return (
    <Box component="form" onSubmit={handleSubmit(onSubmit)}>
      {errorMessage && (
        <Alert severity="error" sx={{ mb: 2 }}>
          {errorMessage}
        </Alert>
      )}

      <TextField
        label="Nombre"
        fullWidth
        margin="normal"
        {...register("fullName", { required: "El nombre es obligatorio" })}
        error={!!errors.fullName}
        helperText={errors.fullName?.message}
        disabled={loading}
      />

      <TextField
        label="Correo"
        type="email"
        fullWidth
        margin="normal"
        {...register("email", {
          required: "El correo es obligatorio",
          pattern: {
            value: /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/,
            message: "Debe ser un correo válido",
          },
        })}
        error={!!errors.email}
        helperText={errors.email?.message}
        disabled={loading}
      />

      <TextField
        label="Contraseña"
        type="password"
        fullWidth
        margin="normal"
        {...register("password", {
          required: "La contraseña es obligatoria",
          pattern: {
            value: /^(?=.*[a-z])(?=.*\d).{6,}$/,
            message:
              "Debe tener mínimo 6 caracteres, al menos una letra minúscula y un número",
          },
        })}
        error={!!errors.password}
        helperText={errors.password?.message}
      />

      <Button
        type="submit"
        variant="contained"
        color="primary"
        fullWidth
        sx={{ mt: 2 }}
        disabled={loading}
        startIcon={loading ? <CircularProgress size={20} /> : null}
      >
        {loading ? "Registrando..." : "Registrarse"}
      </Button>
    </Box>
  );
};

export default RegisterForm;
