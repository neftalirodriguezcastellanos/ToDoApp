import {
  Modal,
  Box,
  TextField,
  Button,
  Typography,
  MenuItem,
  IconButton,
  Fade,
  Backdrop,
} from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";

import {
  useTaskFormModalHandler,
  modalStyle,
  colors,
} from "./TaskFormModalHandler";
import type { Task } from "../../models/Interfaces";

type TaskFormModalProps = {
  open: boolean;
  onClose: () => void;
  onTaskCreated: () => void;
  task?: Task;
};

const TaskFormModal = ({
  open,
  onClose,
  onTaskCreated,
  task,
}: TaskFormModalProps) => {
  const {
    submitError,
    onSubmit,
    isSubmitting,
    errors,
    register,
    handleSubmit,
  } = useTaskFormModalHandler({ onTaskCreated, onClose, task });
  return (
    <Modal
      open={open}
      onClose={onClose}
      closeAfterTransition
      BackdropComponent={Backdrop}
      BackdropProps={{
        timeout: 500,
      }}
    >
      <Fade in={open}>
        <Box sx={modalStyle}>
          <Box
            display="flex"
            justifyContent="space-between"
            alignItems="center"
            mb={2}
          >
            <Typography variant="h6" component="h2">
              {task ? "Editar Tarea" : "Nueva Tarea"}
            </Typography>
            <IconButton onClick={onClose} disabled={isSubmitting}>
              <CloseIcon />
            </IconButton>
          </Box>

          {submitError && (
            <Box mb={2}>
              <Typography color="error" variant="body2">
                {submitError}
              </Typography>
            </Box>
          )}

          <form onSubmit={handleSubmit(onSubmit)}>
            <TextField
              label="Título"
              fullWidth
              margin="normal"
              {...register("title", {
                required: "El título es obligatorio",
                minLength: { value: 2, message: "Mínimo 2 caracteres" },
                maxLength: { value: 100, message: "Máximo 100 caracteres" },
              })}
              error={"title" in errors}
              helperText={errors.title?.message}
              autoFocus
            />
            <TextField
              label="Descripción"
              fullWidth
              multiline
              rows={3}
              margin="normal"
              {...register("description")}
              placeholder="Detalles adicionales (opcional)"
            />
            <TextField
              label="Fecha de vencimiento"
              type="date"
              fullWidth
              margin="normal"
              {...register("dueDate")}
              InputLabelProps={{ shrink: true }}
            />
            <TextField
              select
              label="Color"
              fullWidth
              margin="normal"
              {...register("color")}
              defaultValue="#D3D3D3"
            >
              {colors.map((color) => (
                <MenuItem key={color.value} value={color.value}>
                  <Box
                    sx={{
                      display: "flex",
                      alignItems: "center",
                    }}
                  >
                    <Box
                      sx={{
                        width: 12,
                        height: 12,
                        borderRadius: "50%",
                        backgroundColor: color.value,
                        mr: 1,
                        border: "1px solid #ddd",
                      }}
                    />
                    {color.name}
                  </Box>
                </MenuItem>
              ))}
            </TextField>
            <Box sx={{ mt: 3, display: "flex", gap: 2 }}>
              <Button
                type="button"
                variant="outlined"
                fullWidth
                onClick={onClose}
                disabled={isSubmitting}
              >
                Cancelar
              </Button>
              <Button
                type="submit"
                variant="contained"
                color="primary"
                fullWidth
                disabled={isSubmitting}
              >
                {isSubmitting
                  ? "Guardando..."
                  : `${task ? "Actualizar" : "Crear"} Tarea`}
              </Button>
            </Box>
          </form>
        </Box>
      </Fade>
    </Modal>
  );
};

export default TaskFormModal;
