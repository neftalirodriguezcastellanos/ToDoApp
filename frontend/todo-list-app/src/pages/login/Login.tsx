import {
  Container,
  Paper,
  Box,
  Typography,
  Button,
  TextField,
} from "@mui/material";
import { useForm } from "react-hook-form";
import { useLoginHandler } from "./LoginHandler";
import type { LoginFormData } from "./LoginHandler";
import RegisterForm from "./RegisterForm";

const Login = () => {
  const { loading, handleLogin, handleRegister, isRegister, setIsRegister } =
    useLoginHandler();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormData>({ mode: "onBlur" });

  const onSubmit = (data: LoginFormData) =>
    handleLogin(data.email, data.password);

  return (
    <Container maxWidth="sm">
      <Box
        sx={{
          display: "flex",
          flexDirection: "column",
          minHeight: "100vh",
          justifyContent: "center",
          alignItems: "center",
          px: 2,
        }}
      >
        <Paper
          elevation={3}
          sx={{
            p: 4,
            width: "100%",
            borderRadius: 2,
            boxShadow: "0 8px 24px rgba(0,0,0,0.1)",
          }}
        >
          <Typography
            variant="h5"
            component="h1"
            textAlign="center"
            fontWeight="bold"
            gutterBottom
          >
            ToDo List
          </Typography>

          {!isRegister ? (
            <>
              <Typography
                variant="body2"
                color="text.secondary"
                textAlign="center"
                sx={{ mb: 3 }}
              >
                Accede a tu lista de tareas
              </Typography>

              <form onSubmit={handleSubmit(onSubmit)}>
                <TextField
                  label="Usuario"
                  fullWidth
                  margin="normal"
                  {...register("email", {
                    required: "El correo electrónico es obligatorio",
                  })}
                  error={!!errors.email}
                  helperText={errors.email?.message}
                  disabled={loading}
                />

                <TextField
                  label="Contraseña"
                  type={"password"}
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
                  sx={{ mt: 3 }}
                  disabled={loading}
                >
                  Ingresar
                </Button>
              </form>

              <Button
                fullWidth
                sx={{ mt: 2 }}
                onClick={() => setIsRegister(true)}
              >
                ¿No tienes cuenta? Regístrate
              </Button>
            </>
          ) : (
            <>
              <Typography
                variant="body2"
                color="text.secondary"
                textAlign="center"
                sx={{ mb: 2 }}
              >
                Crea tu cuenta
              </Typography>

              <RegisterForm onRegister={handleRegister} loading={loading} />

              <Button
                fullWidth
                sx={{ mt: 2 }}
                onClick={() => setIsRegister(false)}
              >
                Volver al login
              </Button>
            </>
          )}
        </Paper>
      </Box>
    </Container>
  );
};

export default Login;
