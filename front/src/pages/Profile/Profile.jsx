import { Box, Button, CardMedia, Container, TextField } from '@mui/material';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import validator from 'validator'
import React, { useState } from 'react';
import useForm from '../../hooks/useForm';

const Profile = () => {
  const { formState, onTypingHandler, validityChangeHandler } = useForm();
  const [description, setDescription] = useState();
  const [profileImage, setProfileImage] = useState('https://picsum.photos/318/354');
  const [addAppartmentIsOpen, setAddAppartmentIsOpen] = useState(false);

  // fetch to get user's data, then put it in usestate as a default value. first name, last name etc.

  const profileImageChangeHandler = (event) => {
    console.log(event.target.files[0]);
    let fileURL = URL.createObjectURL(event.target.files[0]);
    setProfileImage(fileURL);
  };

  const saveChangesHandler = () => {

    if (!formState.firstName || !formState.lastName) {
      if (validator.isEmpty(formState.firstName)) {
        validityChangeHandler('isFirstNameValid', false);
      }
      if (validator.isEmpty(formState.lastName)) {
        validityChangeHandler('isLastNameValid', false);
      }
      if (!validator.isEmail(formState.email)) {
        validityChangeHandler('isEmailValid', false);
      }
      // is email valid
      return;
    }
    console.log(description, formState);
  };
  return (
    <Container sx={{ m: 'auto', height: '370' }}>
      <Container
        sx={{
          display: 'flex',
          flexDirection: 'row',
          justifyContent: 'space-between',
          mb: 4,
          p: 2,
          border: '#000000',
        }}
      >
        <Box sx={{ display: 'flex', flexDirection: 'column' }}>
          <CardMedia
            component='img'
            height='300'
            width='300'
            image={profileImage}
            alt='Your Image'
            sx={{ mb: 1 }}
          />
          <Button variant='outlined' component='label'>
            Upload New image
            <input
              type='file'
              hidden
              accept='image/png, image/jpeg'
              onChange={profileImageChangeHandler}
            />
          </Button>
        </Box>
        <Box
          sx={{
            display: 'flex',
            flexDirection: 'column',
            justifyContent: 'space-between',
          }}
        >
          <Box sx={{ display: 'flex', justifyContent: 'space-between', my: 1 }}>
            <TextField
              required
              label='First Name'
              variant='outlined'
              onChange={(event) =>
                onTypingHandler(event, 'firstName', 'isFirstNameValid')
              }
              sx={{ mr: 1 }}
              error={!formState.isFirstNameValid}
            />
            <TextField
              required
              label='Last Name'
              variant='outlined'
              onChange={(event) =>
                onTypingHandler(event, 'lastName', 'isLastNameValid')
              }
              error={!formState.isLastNameValid}
            />
          </Box>
          <TextField
            required
            fullWidth
            label='Email address'
            variant='outlined'
            type='email'
            onChange={(event) =>
              onTypingHandler(event, 'email', 'isEmailValid')
            }
            sx={{ my: 1 }}
          />
          <TextField
            fullWidth
            label='Something About Yourself'
            variant='outlined'
            onChange={(event) => setDescription(event.target.value)}
            multiline
            minRows='2'
            sx={{ my: 1 }}
          />
          <br />
          <Button
            variant='contained'
            onClick={saveChangesHandler}
            sx={{ py: 1 }}
          >
            Save Changes
          </Button>
        </Box>
      </Container>
      <Container sx={{ mb: 4, p: 2, border: '1px solid black' }}>
        <Button
          onClick={() => setAddAppartmentIsOpen((prevState) => !prevState)}
          endIcon={
            !addAppartmentIsOpen ? (
              <KeyboardArrowDownIcon />
            ) : (
              <KeyboardArrowUpIcon />
            )
          }
          sx={{ mb: 2, width: '100%', borderBottom: 1 }}
        >
          My appartment
        </Button>
        {addAppartmentIsOpen && (
          <Container
            sx={{
              display: 'flex',
              flexDirection: 'row',
              justifyContent: 'space-between',
            }}
          >
            <Box sx={{ width: '50%' }}>
              <TextField
                required
                fullWidth
                label='City'
                variant='outlined'
                onChange={(event) =>
                  onTypingHandler(event, 'email', 'isEmailValid')
                }
                sx={{ my: 1 }}
              />
              <TextField
                required
                fullWidth
                label='Address'
                variant='outlined'
                onChange={(event) =>
                  onTypingHandler(event, 'email', 'isEmailValid')
                }
                sx={{ my: 1 }}
              />
              <TextField
                required
                fullWidth
                label='Distance to Center'
                variant='outlined'
                onChange={(event) =>
                  onTypingHandler(event, 'email', 'isEmailValid')
                }
                sx={{ my: 1 }}
              />
              <TextField
                required
                fullWidth
                label='Max number of Guests'
                variant='outlined'
                type='number'
                onChange={(event) =>
                  onTypingHandler(event, 'email', 'isEmailValid')
                }
                sx={{ my: 1 }}
              />
              <TextField
                required
                fullWidth
                label='Description'
                variant='outlined'
                multiline
                minRows='2'
                onChange={(event) =>
                  onTypingHandler(event, 'email', 'isEmailValid')
                }
                sx={{ my: 1 }}
              />
            </Box>
            <Box sx={{ display: 'flex', flexDirection: 'column' }}>
              <CardMedia
                component='img'
                height='300'
                width='300'
                image='https://picsum.photos/318/354'
                alt='Your Image'
                sx={{ mb: 1 }}
              />
              <Button variant='outlined' component='label'>
                Upload New image
                <input type='file' hidden accept='image/png, image/jpeg' />
              </Button>
            </Box>
          </Container>
        )}
      </Container>
    </Container>
  );
};

export default Profile;
