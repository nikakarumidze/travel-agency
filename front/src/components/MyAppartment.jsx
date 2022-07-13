import { Box, Button, Container, TextField, CardMedia } from '@mui/material';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import React from 'react';
import { useState } from 'react';
import { useEffect } from 'react';
import useMyAppartment from '../pages/Profile/hooks/useMyAppartment';

const MyAppartment = () => {
  const [addAppartmentIsOpen, setAddAppartmentIsOpen] = useState(false);
  const [city, setCity] = useState('');
  const [address, setAddress] = useState('');
  const [distance, setDistance] = useState('');
  const [maxGuests, setMaxGuests] = useState('');
  const [description, setDescription] = useState('');
  const [image, setImage] = useState({display: null, send: null});

  const { getMyAppartment, changeApartmentInfo } = useMyAppartment();

  useEffect(() => {
    getMyAppartment().then((data) => {
      if (data) {
        setCity(data.cityName);
        setAddress(data.address);
        setDistance(data.distanceToCenter);
        setMaxGuests(data.maxGuest);
        console.log(data)
        if (data.image) {
          setImage(data.image);
        }
        setDescription(data.description)
      }
    });
  }, []);

  const imageChangeHandler = (event) => {
    const fileURL = URL.createObjectURL(event.target.files[0]);

    let reader = new FileReader();
    reader.readAsDataURL(event.target.files[0]);
    reader.onload = e => {
      setImage({display: fileURL  , send:e.target.result})
    }
  };

  const saveChangesHandler = () => {
    const data = {
      cityName: city,
      address,
      distanceToCenter: distance,
      maxGuest: maxGuests,
      description,
      image: image.send,
      'wifi': true,
      'pool': true,
      'gym': false,
      'conditioner': true,
      'parking': true,
    };
    changeApartmentInfo(data)
      .then((res) => console.log(res))
      .catch((err) => console.log(err, 'sdasd'));
  };

  return (
    <Container sx={{ mb: 4, p: 2 }}>
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
        <Box>
          <Container
            sx={{
              display: 'flex',
              flexDirection: 'row',
              justifyContent: 'space-between',
              mb: 2,
            }}
          >
            <Box sx={{ width: '50%' }}>
              <TextField
                required
                fullWidth
                label='City'
                variant='outlined'
                value={city}
                onChange={(event) => setCity(event.target.value)}
                sx={{ my: 1 }}
              />
              <TextField
                required
                fullWidth
                label='Address'
                variant='outlined'
                value={address}
                onChange={(event) => setAddress(event.target.value)}
                sx={{ my: 1 }}
              />
              <TextField
                required
                fullWidth
                label='Distance to Center'
                variant='outlined'
                value={distance}
                onChange={(event) => setDistance(event.target.value)}
                sx={{ my: 1 }}
              />
              <TextField
                required
                fullWidth
                label='Max number of Guests'
                variant='outlined'
                type='number'
                value={maxGuests}
                onChange={(event) => setMaxGuests(event.target.value)}
                sx={{ my: 1 }}
              />
              <TextField
                required
                fullWidth
                label='Description'
                variant='outlined'
                multiline
                minRows='2'
                value={description}
                onChange={(event) => setDescription(event.target.value)}
                sx={{ my: 1 }}
              />
            </Box>
            <Box sx={{ display: 'flex', flexDirection: 'column' }}>
              <CardMedia
                component='img'
                height='300'
                width='300'
                image={image.display}
                alt='Your Image'
                sx={{ mb: 1 }}
              />
              <Button variant='outlined' component='label'>
                Upload New image
                <input
                  type='file'
                  hidden
                  accept='image/png, image/jpeg'
                  onChange={imageChangeHandler}
                />
              </Button>
            </Box>
          </Container>
          <Button
            variant='contained'
            onClick={saveChangesHandler}
            sx={{ py: 1 }}
            fullWidth
          >
            Save Changes
          </Button>
        </Box>
      )}
    </Container>
  );
};

export default MyAppartment;
